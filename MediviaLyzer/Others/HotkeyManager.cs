using MediviaLyzer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MediviaLyzer.Others
{
    public class HotkeyManager
    {
        public static List<ModifierKeyMap> KeyModifierList { get; } = new List<ModifierKeyMap> {
            new ModifierKeyMap{ Key = Key.LeftAlt, KeyMod = KeyModifier.Alt },
            new ModifierKeyMap{ Key = Key.RightAlt, KeyMod = KeyModifier.Alt },
            new ModifierKeyMap{ Key = Key.LeftCtrl, KeyMod = KeyModifier.Ctrl },
            new ModifierKeyMap{ Key = Key.RightCtrl, KeyMod = KeyModifier.Ctrl },
            new ModifierKeyMap{ Key = Key.LeftShift, KeyMod = KeyModifier.Shift },
            new ModifierKeyMap{ Key = Key.RightShift, KeyMod = KeyModifier.Shift }};
    }
    public class ModifierKeyMap
    {
        public Key Key { get; set; }
        public KeyModifier KeyMod { get; set; }
    }
    [Flags]
    public enum KeyModifier
    {
        None = 0x0000,
        Alt = 0x0001,
        Ctrl = 0x0002,
        Shift = 0x0004,
        Win = 0x0008,
        NoRepeat = 0x4000
    }
}
