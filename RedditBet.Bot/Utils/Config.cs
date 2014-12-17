using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;
using RedditBet.Bot.Tasks;
using RestSharp;

namespace RedditBet.Bot.Utils
{
    public static class Config
    {
        // Private fields

        private static string _baseUrl { get { return ConfigurationManager.AppSettings["baseUrl"]; } }
        private static string _subReddit { get { return ConfigurationManager.AppSettings["subReddit"]; } }
        private static string _apiBaseUrl { get { return ConfigurationManager.AppSettings["apiBaseUrl"]; } }
        private static string _filter { get { return ConfigurationManager.AppSettings["crawlerFilter"]; } }
        private static string _api_Tasks { get { return ConfigurationManager.AppSettings["api_Tasks"]; } }
        private static string _api_Tasks_Incomplete { get { return ConfigurationManager.AppSettings["api_Tasks_Incomplete"]; } }
                
        // Public methods

        public static List<string> GetCrawlerUrls()
        {
            var requester = new Requester(string.Format("{0}{1}.json?limit=100", _baseUrl, _subReddit));
            var response = requester.GetResponse();
            var json = JsonConvert.DeserializeObject<RedditJSON>(response.Content);

            var urls = new List<string>();

            foreach (var item in json.data.children)
            {
                urls.Add(string.Format("{0}{1}", _baseUrl, item.data.permalink));
            }
            
            return urls;
        }

        public static Dictionary<string, double> GetTargetWords()
        {
            var json = JsonConvert.DeserializeObject<Words>(RedditBet.Bot.Properties.Resources.words);
            return json.words;
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
