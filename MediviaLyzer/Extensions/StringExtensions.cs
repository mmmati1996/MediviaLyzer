using MediviaLyzer.Models;
using MediviaLyzer.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace MediviaLyzer.Extensions
{
    public static class StringExtensions
    {
        public static HotkeyModel ConvertToHotkeyModel(this string hotkeyStr)
        {
            var model = new HotkeyModel();
            Regex rgx = new Regex(@"\{(.*?)\}");

            foreach (var match in rgx.Matches(hotkeyStr))
            {
                Key key;
                Enum.TryParse(match.ToString().Substring(1, match.ToString().Length-2), out key);
                var isModifier = HotkeyManager.KeyModifierList.Where(x => x.Key == key).FirstOrDefault();
                if (isModifier != null)
                    model.Modifiers.Add(isModifier);
                else
                    model.Keys.Add(key);
            }
            return model;
        }
    }
}
