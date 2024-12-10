using System;
using System.Collections.Generic;
using System.IO;
using KirovAir.Core.Config;
using SixLabors.ImageSharp;
using TwilightBoxart.Models.Base;

namespace TwilightBoxart
{
    public class BoxartConfig : IniSettings
    {
        public string SdRoot { get; set; } = "";
        public string BoxartPath { get; set; } = @"{sdroot}\_nds\TWiLightMenu\boxart";
        public int BoxartWidth { get; set; } = 128;
        public int BoxartHeight { get; set; } = 115;
        public bool AdjustAspectRatio { get; set; } = true;

        public const string MagicDir = "_nds";
        public const string FileName = "TwilightBoxart.ini";
        public static string Credits = "TwilightBoxart - Développé par KirovAir, Traduit en français par Rinzler, portage .NET 8 réalisé par MateusRodCosta,." + Environment.NewLine + "Un grand merci aux développeurs de TwilightMenu++, LibRetro, GameTDB et aux responsables de la base de données No-Intro.";

        public void Load()
        {
            Load(FileName);
        }

        public string GetBoxartPath(string root = "")
        {
            if (root == "")
            {
                root = SdRoot;
            }

            if (!BoxartPath.StartsWith("{sdroot}"))
            {
                var split = BoxartPath.Split(['/', '\\']);
                return Path.Combine(split);
            }

            if (root.Contains('/') || root.Contains('\\'))
            {
                try
                {
                    var split = root.Split(['/', '\\']);
                    var tmpReplace = "";
                    for (var i = split.Length; i-- > 0;)
                    {
                        tmpReplace = split[i] + Path.DirectorySeparatorChar + tmpReplace;
                        tmpReplace = tmpReplace.TrimEnd(Path.DirectorySeparatorChar);

                        // Remove where we are.
                        var place = root.LastIndexOf(tmpReplace);
                        if (place == -1)
                            break;
                        var correctRoot = root.Remove(place, tmpReplace.Length);

                        if (Directory.Exists(Path.Combine(correctRoot, MagicDir)))
                        {
                            root = correctRoot;
                            break;
                        }
                    }
                }
                catch { }
            }

            return Path.Combine(root.TrimEnd(Path.DirectorySeparatorChar), BoxartPathNormalized().Replace("{sdroot}", "").TrimStart(Path.DirectorySeparatorChar));
        }

        // Used as backup mapping.
        public static readonly Dictionary<string, ConsoleType> ExtensionMapping = new()
        {
            {".nes", ConsoleType.NintendoEntertainmentSystem},
            {".sfc", ConsoleType.SuperNintendoEntertainmentSystem},
            {".smc", ConsoleType.SuperNintendoEntertainmentSystem},
            {".snes", ConsoleType.SuperNintendoEntertainmentSystem},
            {".gb", ConsoleType.GameBoy},
            {".sgb", ConsoleType.GameBoy},
            {".gbc", ConsoleType.GameBoyColor},
            {".gba", ConsoleType.GameBoyAdvance},
            {".nds", ConsoleType.NintendoDS},
            {".ds", ConsoleType.NintendoDS},
            {".dsi", ConsoleType.NintendoDSi},
            {".gg", ConsoleType.SegaGameGear},
            {".gen", ConsoleType.SegaGenesis},
            {".sms", ConsoleType.SegaMasterSystem},
            {".fds", ConsoleType.FamicomDiskSystem},
            {".zip", ConsoleType.Unknown }
        };

        /// <summary>
        /// Mapping to merge some ConsoleTypes in the DB.
        /// </summary>
        public static Dictionary<ConsoleType, ConsoleType> NoIntroDbMapping = new()
        {
            {ConsoleType.NintendoDS, ConsoleType.NintendoDS},
            {ConsoleType.NintendoDSDownloadPlay, ConsoleType.NintendoDS},
            {ConsoleType.NintendoDSi, ConsoleType.NintendoDSi},
            {ConsoleType.NintendoDSiDigital, ConsoleType.NintendoDSi}
        };

        public static Dictionary<ConsoleType, Size> AspectRatioMapping = new()
        {
            // FDS / GBC / GB
            {ConsoleType.FamicomDiskSystem, new Size(1, 1)},
            {ConsoleType.GameBoy, new Size(1, 1)},
            {ConsoleType.GameBoyColor, new Size(1, 1)},
            {ConsoleType.GameBoyAdvance, new Size(1, 1)},

            // NES / GEN/MD / SFC / MS/ GG
            {ConsoleType.NintendoEntertainmentSystem, new Size(84, 115)},
            {ConsoleType.SegaGenesis, new Size(84, 115)},
            {ConsoleType.SegaMasterSystem, new Size(84, 115)},
            {ConsoleType.SegaGameGear, new Size(84, 115)},

            // SNES
            {ConsoleType.SuperNintendoEntertainmentSystem, new Size(158, 115)}
        };

        public static Dictionary<ConsoleType, string> LibRetroDatUrls = new()
        {
            {ConsoleType.NintendoEntertainmentSystem, "https://github.com/libretro/libretro-database/raw/master/dat/Nintendo%20-%20Nintendo%20Entertainment%20System.dat"}
        };


        private String BoxartPathNormalized()
        {
            var split = this.BoxartPath.Split(['/', '\\']);
            var BoxartPath = Path.Combine(split);

            return BoxartPath;
        }
    }
}
