using System;
using System.Collections.Generic;
using Newtonsoft;
using Newtonsoft.Json;

namespace RedditBet.Bot.DataResources
{
    using RedditSharp;
    using RedditBet.Bot.Tasks;
    using RedditBet.Bot.Utils;
    using RedditBet.Bot.Models;
    using Local = RedditBet.Bot.Properties;

    public static class Data
    {
        #region Local Resources 

        /// <summary>
        /// Gets all the URLs to crawl.
        /// </summary>
        /// <returns>A List of URLs</returns>
        public static List<string> GetCrawlerUrls()
        {
            var requester = new Requester(string.Format("{0}/r/{1}.json?limit={2}", Config.BaseUrl, Config.SubReddit, Config.UrlLimit));
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
        public static ICollection<string> GetPhrasesToMatch()
        {
            var json = JsonConvert.DeserializeObject<Phrases>(RedditBet.Bot.Properties.Resources.phrases);

            return json.phrases;
        }

        // Markdown files

        public static string MarkDown_Test = Local.Resources.test;
                
        #endregion

        #region BetBot.API

        /// <summary>
        /// Todo
        /// </summary>
        /// <returns></returns>
        public static List<IBotTask> GetIncompleteTasks()
        {
            var requester = new Requester(string.Format("{0}{1}", Config.ApiUrl, Config.Api_Tasks_GetIncomplete));
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
        public static void SaveMatchedComments(Comments comments)
        {
            foreach (var comment in comments)
            {
                var task = comment.ToBotTask(TaskType.Reply);
                var requester = new Requester(string.Format("{0}{1}", Config.ApiUrl, Config.Api_Tasks), RequestMethod.POST, task);
                var response = requester.GetResponse();
                var statusCode = response.StatusCode;
            }
        }

        public static void MarkCommentComplete(int id)
        {
            var requester = new Requester(string.Format("{0}{1}/{2}", Config.ApiUrl, Config.Api_Tasks_MarkTaskComplete, id), RequestMethod.POST);
            var response = requester.GetResponse();
            var statusCode = response.StatusCode;
        }

        public static void AddLog(LogModel log)
        { 
            var requester = new Requester(string.Format("{0}{1}", Config.ApiUrl, Config.Api_Log), RequestMethod.POST, log);
            var response = requester.GetResponse();
            var statusCode = response.StatusCode;
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
            
            reddit.AddUserAgent(Config.Bot_UserAgent);

            var user = reddit.LogIn(username, password);
            
            if (user != null)
            {
                Log.Info(string.Format("Successfully logged-in as {0}", user.FullName));
            }

            return reddit;
        }
    }
}
