using System;
using System.Collections.Generic;
using Newtonsoft;
using Newtonsoft.Json;
using RedditBet.Bot.Tasks;
using RedditBet.Bot.Utils;
using RedditBet.Bot.Models;
using RedditBet.Bot.Enums;
using RedditSharp;

namespace RedditBet.Bot.DataHelpers
{
    public static class Api
    {
        #region Local Resources 

        /// <summary>
        /// Gets all the URLs to crawl.
        /// </summary>
        /// <returns>A List of URLs</returns>
        public static List<string> GetCrawlerUrls()
        {
            var requester = new Requester(string.Format("{0}/r/{1}.json?limit=100", Config.BaseUrl, Config.SubReddit));
            var response = requester.GetResponse();
            var json = JsonConvert.DeserializeObject<RedditJSON>(response.Content);
            var urls = new List<string>();

            foreach (var item in json.data.children)
            {
                urls.Add(string.Format("{0}{1}", Config.BaseUrl, item.data.permalink));
            }

            // urls.Add("http://rc.reddit.com/r/CFB/comments/1rkt6s/week_14_user_friendly_bet_thread/");

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

        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="comments"></param>
        public static void AddUpdateComments(Comments comments)
        {
            foreach (var comment in comments)
            {
                var task = comment.ToBotTask(TaskType.Reply);
                var requester = new Requester(string.Format("{0}{1}", Config.ApiUrl, Config.Api_Tasks), RequestMethod.POST, task);
                var response = requester.GetResponse();
                
                // if (response.StatusCode == OK)
            }
        }

        #endregion
    }

    /// <summary>
    /// Returns a new RedditSharp "Reddit" instance
    /// </summary>
    public static class RedditApi
    {
        public static Reddit Init(string username, string password)
        {
            var reddit = new Reddit();
            var user = reddit.LogIn(username, password);

            return reddit;
        }
    }
}
