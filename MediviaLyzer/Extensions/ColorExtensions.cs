using System;
using MColor = System.Windows.Media.Color;
using DColor = System.Drawing.Color;

namespace MediviaLyzer.Extensions
{
    public static class ColorExtensions
    {
        public static MColor ToMediaColor(this DColor color)
        {
            return MColor.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}
