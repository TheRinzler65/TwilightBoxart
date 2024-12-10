using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KirovAir.Core.ConsoleApp;
using KirovAir.Core.Utilities;

namespace TwilightBoxart.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ConsoleEx.WriteGreenLine(BoxartConfig.Credits);
            Console.WriteLine();

            var config = new BoxartConfig();
            try
            {
                config.Load();
            }
            catch { Console.WriteLine("Impossible de charger TwilightBoxart.ini - utilisation des paramètres par défaut."); }

            if (string.IsNullOrEmpty(config.SdRoot))
            {
                var allDrives = new List<DriveInfo>();
                try
                {
                    allDrives = DriveInfo.GetDrives().Where(c => c.DriveType == DriveType.Removable).ToList();
                }
                catch { }

                var choice = FileHelper.GetCurrentDirectory();
                if (allDrives.Count > 0)
                {
                    var choices = allDrives.Select(c => c.Name).ToList();
                    choices.Add("Répertoire actuel");
                    var i = ConsoleEx.MenuIndex("Sélectionnez l'emplacement de votre carte SD : ", true, choices.ToArray());
                    if (i != choices.Count - 1)
                    {
                        choice = allDrives[i].RootDirectory.FullName;
                    }
                }
                else
                {
                    Console.WriteLine("Aucun paramètre ni lecteur trouvé. Utilisation du répertoire actuel.");
                }

                config.SdRoot = choice;
            }

            var boxArtPath = config.GetBoxartPath();
            ConsoleEx.WriteGreenLine("Paramètres chargés :");
            Console.WriteLine("Emplacement Racine SD / Roms : \t" + config.SdRoot);
            Console.WriteLine("Emplacement de la BoxArt : \t\t" + boxArtPath);
            Console.WriteLine();
            if (!ConsoleEx.YesNoMenu("Est-ce que cela vous convient ?"))
            {
                Console.WriteLine("Veuillez modifier le fichier TwilightBoxart.ini ou insérer votre carte SD et réessayer.");
                return;
            }
            Console.WriteLine();

            var progress = new Progress<string>(Console.WriteLine);
            var crawler = new BoxartCrawler(progress);
            crawler.InitializeDb();
            await crawler.DownloadArt(config.SdRoot, boxArtPath, config.BoxartWidth, config.BoxartHeight, config.AdjustAspectRatio);
        }

        // Todo: Implement as CLI and add Progress<> to MetaCrawler.
        //public void AddMeta()
        //{
        //    Console.WriteLine("This program will generate a sha1/title/id DB based on the path:");
        //    Console.WriteLine("");
        //    if (!ConsoleEx.YesNoMenu("Start now?"))
        //        return;
        //    var crawler = new LocalMetaCrawler("", "");
        //    crawler.Go();
        //    Console.WriteLine("Done.");
        //}
    }
}
