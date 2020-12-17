using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

namespace MediviaLyzer.Database
{
    class WikiScrapper
    {
        private string BaseUrlAddress = "http://wiki.mediviastats.info/";
        private HtmlAgilityPack.HtmlWeb WebSite;

        public WikiScrapper()
        {
            this.WebSite = new HtmlAgilityPack.HtmlWeb();
        }

        public void Test()
        {
            try
            {
                WebClient webClient = new WebClient();
                string page = webClient.DownloadString(BaseUrlAddress+"Blue_Hood");
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(page);
                List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@class='wikitable']")
                .Descendants("tr")
                .Skip(1)
                .Where(tr => tr.Elements("td").Count() >= 1)
                .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                .ToList();

                foreach (var el in table)
                    foreach (var el1 in el)
                        Debug.WriteLine(el1);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
