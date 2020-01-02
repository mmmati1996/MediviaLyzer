using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MediviaLyzer.Others
{
    class WebScrapping
    {
        private string BaseUrlAddress = "https://medivia.online/community/character/";
        private HtmlAgilityPack.HtmlWeb WebSite;

        public WebScrapping()
        {
            this.WebSite = new HtmlAgilityPack.HtmlWeb();
        }

        public bool CheckIfCharacterExist(string charactername)
        {
            try
            {
                HtmlAgilityPack.HtmlDocument doc = WebSite.Load(BaseUrlAddress + charactername);
                var headername = doc.DocumentNode.SelectNodes("//div[@class='med-width-100 med-mt-10']").ToList();
                if (headername != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public uint GetLastLogin(string charactername)
        {
            try
            {
                HtmlAgilityPack.HtmlDocument doc = WebSite.Load(BaseUrlAddress + charactername);
                var headername = doc.DocumentNode.SelectNodes("//div[@class='med-width-100 med-mt-10']").ToList();
                foreach (var head in headername)
                    if (CheckIfLastLogin_Regex(head.InnerText))
                        return GetIntFromText(head.InnerText);
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        private bool CheckIfLastLogin_Regex(string innertext)
        {
            string pattern = "last login:.*";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(innertext))
                return true;
            else
                return false;
        }

        private uint GetIntFromText(string text)
        {
            Match match = Regex.Match(text, @"\d+");
            if (match.Success)
            {
                return Convert.ToUInt32(match.Value);
            }
            else
                return 0;
        }
    }
}
