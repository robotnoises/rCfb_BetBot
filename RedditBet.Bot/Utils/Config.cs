using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;
using RedditBet.Bot.Models;

namespace RedditBet.Bot.Utils
{
    public static class Config
    {
        // Private fields

        private static string _baseUrl { get { return ConfigurationManager.AppSettings["baseUrl"]; } }
        private static string _apiBaseUrl { get { return ConfigurationManager.AppSettings["apiBaseUrl"]; } }
        private static string _filter { get { return ConfigurationManager.AppSettings["crawlerFilter"]; } }

        private static string _api_Tasks { get { return ConfigurationManager.AppSettings["api_Tasks"]; } }
        private static string _api_Tasks_Incomplete { get { return ConfigurationManager.AppSettings["api_Tasks_Incomplete"]; } }
                
        // Public methods

        public static List<string> GetCrawlerUrls()
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

        public static Dictionary<string, double> GetTargetWords()
        {
            var json = JsonConvert.DeserializeObject<Words>(RedditBet.Bot.Properties.Resources.words);
            return json.words;
        }

        public static string Api_Base()
        {
            return _apiBaseUrl;
        }

        public static string Api_Tasks(bool getAll)
        {
            if (getAll)
            {
                return _api_Tasks;
            }
            else
            {
                return _api_Tasks_Incomplete;
            }
        }

        // Private methods

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
