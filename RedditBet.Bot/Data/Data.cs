using System;
using System.Collections.Generic;
using Newtonsoft;
using Newtonsoft.Json;
using RedditBet.Bot.Tasks;
using RedditBet.Bot.Utils;
using RedditBet.Bot.Models;

namespace RedditBet.Bot.DataHelpers
{
    public static class Data
    {
        #region Local Resources 

        /// <summary>
        /// Gets all the URLs to crawl.
        /// </summary>
        /// <returns>A List of URLs</returns>
        public static List<string> GetCrawlerUrls()
        {
            var requester = new Requester(string.Format("{0}{1}.json?limit=100", Config.BaseUrl, Config.SubReddit));
            var response = requester.GetResponse();
            var json = JsonConvert.DeserializeObject<RedditJSON>(response.Content);
            var urls = new List<string>();

            foreach (var item in json.data.children)
            {
                urls.Add(string.Format("{0}{1}", Config.BaseUrl, item.data.permalink));
            }

            return urls;
        }

        /// <summary>
        /// Gets a Dictionary of key words, to be matched within blocks of text. Each has an associated value.
        /// </summary>
        /// <returns>A Dictionary of key words and their values</returns>
        public static Dictionary<string, double> GetMatchWords()
        {
            var json = JsonConvert.DeserializeObject<Words>(RedditBet.Bot.Properties.Resources.words);
            return json.words;
        }

        #endregion

        #region BetBot.API

        /// <summary>
        /// Todo
        /// </summary>
        /// <returns></returns>
        public static List<IBotTask> GetIncompleteTasks()
        {
            var requester = new Requester(string.Format("{0}{1}", Config.ApiUrl, Config.Api_Tasks_Incomplete));
            var response = requester.GetResponse();
            var json = JsonConvert.DeserializeObject<List<BotTask>>(response.Content);
            var tasks = new List<IBotTask>();

            foreach (var task in json)
            {
                tasks.Add(TaskFactory.Create(task));
            }

            return tasks;
        }

        #endregion
    }
}
