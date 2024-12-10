using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KirovAir.Core.Extensions;
using KirovAir.Core.Utilities;
using TwilightBoxart.Crawlers.LibRetro;
using TwilightBoxart.Crawlers.NoIntro;
using TwilightBoxart.Models.Base;
using Utf8Json;

namespace TwilightBoxart.Data
{
    public class RomDatabase(string databasePath)
    {
        private readonly string _databasePath = databasePath;
        private List<RomMetaData> _roms = [];
        private readonly RetryHelper _retryHelper = new(TimeSpan.FromSeconds(3));

        public void Initialize(IProgress<string> progress = null)
        {
            if (File.Exists(_databasePath))
            {
                try
                {
                    using var compressed = File.OpenRead(_databasePath);
                    _roms = JsonSerializer.Deserialize<List<RomMetaData>>(FileHelper.Decompress(compressed));
                }
                catch (Exception e)
                {
                    progress?.Report("Erreur de lecture de la base de données NoIntro : " + e);
                }
            }

            if (_roms == null || _roms.Count == 0)
            {
                progress?.Report("Aucune base de données valide n'a été trouvée ! Téléchargement de la base de données No-Intro...");
                _roms = [];

                foreach (var consoleType in (ConsoleType[])Enum.GetValues(typeof(ConsoleType)))
                {
                    if (consoleType == ConsoleType.Unknown)
                        continue;

                    progress?.Report($"{consoleType.GetDescription()}.. ");

                    if (!BoxartConfig.NoIntroDbMapping.TryGetValue(consoleType, out var baseType))
                    {
                        baseType = consoleType; // If nothing is found set the same type.
                    }

                    DataFile data = null;
                    _retryHelper.RetryOnException(() =>
                    {
                        data = NoIntroCrawler.GetDataFile(consoleType).Result;
                    });

                    foreach (var game in data.Game)
                    {
                        var rom = new RomMetaData
                        {
                            ConsoleType = baseType,       // The base type to merge similar searches. (nds sha1 might be in a dsi library)
                            ConsoleSubType = consoleType, // The 'original' type.
                            GameId = game.Game_id,
                            Name = game.Name,
                            Serial = game.Rom?.Serial,
                            Sha1 = game.Rom?.Sha1.ToLower(),
                            Status = game.Rom?.Status
                        };
                        _roms.Add(rom);
                    }

                    progress?.Report($"{data.Game.Count} roms trouvées");
                }

                progress?.Report("Téléchargement des données supplémentaires LibRetro...");

                foreach (var map in BoxartConfig.LibRetroDatUrls)
                {
                    progress?.Report($"{map.Key.GetDescription()}.. ");

                    List<LibRetroRomData> data = null;
                    _retryHelper.RetryOnException(async () => { data = await LibRetroDat.DownloadDat(map.Value); });

                    foreach (var game in data)
                    {
                        var rom = new RomMetaData
                        {
                            ConsoleType = map.Key,
                            ConsoleSubType = map.Key,
                            Name = game.Name,
                            Sha1 = game.RomSha1.ToLower()
                        };
                        _roms.Add(rom);
                    }

                    progress?.Report($"{data.Count} roms trouvées");
                }

                progress?.Report("Vidage des données...");
                using (var ms = new MemoryStream())
                {
                    JsonSerializer.Serialize(ms, _roms);
                    ms.Seek(0, SeekOrigin.Begin);
                    File.WriteAllBytes(_databasePath, FileHelper.Compress(ms));
                }

                progress?.Report("Terminé !");
            }

            progress?.Report($"Base de données NoIntro.db chargée. La base de données contient {_roms?.Count} roms.");
        }

        public void AddMetadata(IRom rom)
        {
            RomMetaData result = null;
            if (!string.IsNullOrEmpty(rom.Sha1))
            {
                result = _roms.FirstOrDefault(c => c.Sha1.Equals(rom.Sha1, StringComparison.OrdinalIgnoreCase));
            }

            if (result == null && !string.IsNullOrEmpty(rom.TitleId))
            {
                var results =
                 _roms.Where(c => c.ConsoleType == rom.ConsoleType &&
                                (
                                    (c.Serial?.Equals(rom.TitleId, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                    (c.TitleId?.Equals(rom.TitleId, StringComparison.OrdinalIgnoreCase) ?? false)
                                )).ToList();

                // Do logic to fix dupes.. Todo: make this actually solid but for now we filter bad dumps.
                if (results.Count > 1)
                {
                    result = results.FirstOrDefault(c => !c.Status?.Contains("bad") ?? true); // lol
                }
                else
                {
                    result = results.FirstOrDefault();
                }
            }

            if (result == null) return;

            rom.NoIntroName = result.Name;
            rom.NoIntroConsoleType = result.ConsoleSubType;
        }
    }
}
