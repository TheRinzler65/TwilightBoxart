using KirovAir.Core.Extensions;
using System;
using System.Threading.Tasks;
using TwilightBoxart.Models.Base;

namespace TwilightBoxart.Models
{
    public class NdsRom : LibRetroRom
    {
        public override ConsoleType ConsoleType => ConsoleType.NintendoDS;
        public char RegionId { get; set; }

        public NdsRom(byte[] header)
        {
            Title = header.GetString(0, 12);
            TitleId = header.GetString(12, 4);
            RegionId = (char)header[15];
        }

        public override async Task DownloadBoxArt(string targetFile)
        {
            try
            {
                var region = GetUrlRegion(); // Try correct region
                try
                {
                    await DownloadAndResize(region, targetFile);
                }
                catch (Exception e)
                {
                    if (region != "EN" && e.Message.Contains("404")) // Finally, try EN region.
                    {
                        await DownloadAndResize("EN", targetFile);
                        return;
                    }

                    throw;
                }
            }
            catch
            {
                await base.DownloadBoxArt(targetFile);
            }
        }

        private static readonly string[] Qualities = ["HQ", "M", "S"];
        private static readonly string[] Extensions = ["jpg", "jpg", "png"];
        private async Task DownloadAndResize(string region, string targetFile)
        {
            var lastException = new Exception("Impossible de télécharger la région. " + region);
            for (var i = 0; i < Qualities.Length; i++)
            {
                var quality = Qualities[i];
                var extension = Extensions[i];
                var url = $"https://art.gametdb.com/ds/cover{quality}/{region}/{TitleId}.{extension}";

                try
                {
                    await ImgDownloader.DownloadAndResize(url, targetFile);
                }
                catch (Exception e)
                {
                    lastException = e;
                }
            }

            throw lastException;
        }

        private string GetUrlRegion()
        {
            var region = "EN";
            switch (RegionId)
            {
                case 'E':
                case 'T':
                    region = "US";   // USA
                    break;
                case 'J':
                    region = "JA";   // Japanese
                    break;
                case 'K':
                    region = "KO";   // Korean
                    break;

                case 'O':           // USA/Europe
                    region = "EN";
                    break;
                case 'P':           // Europe


                case 'U':
                    // Alternate country code for Australia.
                    region = "EN";
                    break;

                // European country-specific localizations.
                case 'D':
                    region = "DE";   // German
                    break;
                case 'F':
                    region = "FR";   // French
                    break;
                case 'H':
                    region = "NL";   // Dutch
                    break;
                case 'I':
                    region = "IT";   // Italian
                    break;
                case 'R':
                    region = "RU";   // Russian
                    break;
                case 'S':
                    region = "ES";   // Spanish
                    break;
                case '#':
                    region = "HB"; // Homebrew
                    break;
            }

            return region;
        }
    }
}
