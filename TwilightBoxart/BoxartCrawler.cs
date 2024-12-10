using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using KirovAir.Core.Utilities;
using SixLabors.ImageSharp;
using TwilightBoxart.Data;
using TwilightBoxart.Helpers;
using TwilightBoxart.Models.Base;

namespace TwilightBoxart
{
    public class BoxartCrawler
    {
        private readonly IProgress<string> _progress;
        private static RomDatabase _romDb;

        public BoxartCrawler(IProgress<string> progress = null)
        {
            // Disable all SSL cert pinning for now as users have reported problems with github.
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            _progress = progress;
            _romDb = new RomDatabase(Path.Combine(FileHelper.GetCurrentDirectory(), "NoIntro.db"));
        }

        public void InitializeDb()
        {
            _romDb.Initialize(_progress);
        }

    public async Task DownloadArt(string romsPath, string boxArtPath, int defaultWidth, int defaultHeight, bool useAspect = false)
    {
        _progress?.Report($"Analyse de {romsPath}...");

        try
        {
            if (!Directory.Exists(romsPath))
            {
                _progress?.Report($"Impossible d'ouvrir {romsPath}.");
                return;
            }

            foreach (var romFile in Directory.EnumerateFiles(romsPath, "*.*", SearchOption.AllDirectories))
            {
                var ext = Path.GetExtension(romFile).ToLower();
                if (!BoxartConfig.ExtensionMapping.ContainsKey(ext))
                    continue;

                var targetArtFile = Path.Combine(boxArtPath, Path.GetFileName(romFile) + ".png");
                if (File.Exists(targetArtFile))
                {
                    // We already have it.
                    _progress?.Report($"Ignorer {Path.GetFileName(romFile)}... (Nous l'avons déjà)");
                    continue;
                }

                try
                {
                    _progress?.Report($"Recherche de l'illustration pour {Path.GetFileName(romFile)}... ");

                    var rom = Rom.FromFile(romFile);
                    _romDb.AddMetadata(rom);

                    var downloader = new ImgDownloader(defaultWidth, defaultHeight);
                    if (useAspect && BoxartConfig.AspectRatioMapping.TryGetValue(rom.ConsoleType, out var size))
                    {
                        if (rom.ConsoleType == ConsoleType.SuperNintendoEntertainmentSystem)
                        {
                            if ((rom.NoIntroName?.ToLower().Contains("(japan)", StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (rom.SearchName?.ToLower().Contains("(japan)", StringComparison.OrdinalIgnoreCase) ?? false))
                            {
                                size = new Size(84, 115);
                            }
                        }
                        downloader.SetSizeAdjustedToAspectRatio(size);
                    }

                    rom.SetDownloader(downloader);

                    Directory.CreateDirectory(Path.GetDirectoryName(targetArtFile));
                    await rom.DownloadBoxArt(targetArtFile);
                    _progress?.Report("Got it!");
                }
                catch (NoMatchException ex)
                {
                    _progress?.Report(ex.Message);
                }
                catch (Exception e)
                {
                    _progress?.Report("Une erreur est survenue : " + e.Message);
                }
            }

            _progress?.Report("Analyse terminée.");
        }
        catch (Exception e)
        {
            _progress?.Report("Une exception non gérée s'est produite ! " + e);
        }
    }
}
}
