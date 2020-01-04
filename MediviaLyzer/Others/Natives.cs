using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MediviaLyzer.Others
{
    public static class Natives
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
    }
}
