﻿using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using TwilightBoxart.Models.Base;

namespace TwilightBoxart.Crawlers.LocalMeta
{
    /// <summary>
    /// Will create a DB list of local titles to add extra info to the NoIntro DBs. (Title + ID)
    /// </summary>
    public class LocalMetaCrawler(string dir, string outputFile)
    {
        private readonly string _dir = dir;
        private readonly string _outputFile = outputFile;

        private int _count;

        public void Go()
        {
            foreach (var romFile in Directory.EnumerateFiles(_dir, "*.*", SearchOption.AllDirectories))
            {
                var ext = Path.GetExtension(romFile).ToLower();
                if (ext == ".zip")
                {
                    using var fs = File.OpenRead(romFile);
                    using var archive = new ZipArchive(fs);
                    var romEntry = archive.Entries.FirstOrDefault(c => BoxartConfig.ExtensionMapping.ContainsKey(Path.GetExtension(c.Name)));
                    if (romEntry != null)
                    {
                        using var ms = new MemoryStream();
                        using var dec = romEntry.Open();
                        dec.CopyTo(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        try
                        {
                            var fsRom = Rom.FromStream(ms, romEntry.FullName);
                            WriteRom(fsRom);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Erreur avec : " + romEntry.FullName);
                            WriteErr(romFile, e);
                        }
                    }

                    continue;
                }

                if (!BoxartConfig.ExtensionMapping.ContainsKey(ext))
                    continue;

                try
                {
                    var rom = Rom.FromFile(romFile);
                    WriteRom(rom);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erreur avec : " + romFile);
                    WriteErr(romFile, e);
                }
            }
        }

        public void WriteErr(string romFile, Exception e)
        {
            File.AppendAllText(_outputFile + "_err.txt", romFile + "\n" + e + "\n\n");
        }

        public void WriteRom(IRom rom)
        {
            _count++;
            var d = $"{rom.ConsoleType}|\n{rom.Sha1}|\n{rom.Title}|\n{rom.TitleId}|\n\n";
            if (_count % 10 == 0)
            {
                Console.Title = _count.ToString();
                Console.WriteLine(d.Replace('\n', ' '));
            }

            File.AppendAllText(_outputFile, d);
        }
    }
}
