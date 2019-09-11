﻿using KirovAir.Core.Extensions;
using TwilightBoxart.Models.Base;

namespace TwilightBoxart.Models
{
    public class GbRom : LibRetroRom
    {
        public override ConsoleType ConsoleType => ConsoleType.Gb;

        public GbRom(byte[] header)
        {
            Title = header.GetString(308, 12);
            TitleId = header.GetString(320, 4);
        }
    }
}