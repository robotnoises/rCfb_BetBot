using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Configuration;
using System.Net;

namespace RedditBet.Bot.Utils
{
    public static class Config
    {
        private static string _baseUrl { get { return ConfigurationManager.AppSettings["baseUrl"]; } }
        private static string _filter { get { return ConfigurationManager.AppSettings["crawlerFilter"]; } }

        public static List<string> GetUrls()
        {
            var urls = new List<string>();
            var doc = new HtmlDocument();
            doc.LoadHtml(GetPage());

            foreach (var node in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                var href = node.Attributes["href"].Value;
                if (href.Contains(_filter) && href.Contains("http://"))
                {
                    urls.Add(href);
                }
            }

            return urls;
        }

        private static string GetPage(string url = null)
        {
            var urlToGet = url ?? _baseUrl;

            using (var client = new WebClient())
            {
                return client.DownloadString(urlToGet);
            }
        }
    }
}
