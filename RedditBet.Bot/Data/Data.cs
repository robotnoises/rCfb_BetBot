﻿using System;
using System.Collections.Generic;
using Newtonsoft;
using Newtonsoft.Json;

namespace RedditBet.Bot.DataResources
{
    using RedditSharp;
    using RedditComment = RedditSharp.Things.Comment;
    using RedditBet.Bot.Tasks;
    using RedditBet.Bot.Utils;
    using RedditBet.Bot.Models;

    public static class Api
    {
        #region BetBot.API

        /// <summary>
        /// Todo
        /// </summary>
        /// <returns></returns>
        internal static List<IBotTask> GetIncompleteTasks()
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
        internal static void AddReplyTasks(Comments comments)
        {
            foreach (var comment in comments)
            {
                var task = comment.ToBotTask(TaskType.Reply);

                AddTask(task);
            }
        }

        // Todo: This needs a lot of TLC...
        internal static void AddMonitorTask(string userName, string linkToMonitor)
        {
            var task = new BotTask();
            var data = new TaskData();

            data.Add(new TaskDataItem(Config.Username_Key, userName));
            data.Add(new TaskDataItem(Config.TargetUrl_Key, linkToMonitor));
            
            task.TaskType = TaskType.Monitor;
            task.Data = data;
            task.Completed = false;
            
            AddTask(task);
        }

        private static void AddTask(BotTask task)
        {
            var requester = new Requester(string.Format("{0}{1}", Config.ApiUrl, Config.Api_Tasks), RequestMethod.POST, task);
            var response = requester.GetResponse();
            var statusCode = response.StatusCode;
        }

        internal static void MarkTaskComplete(int id)
        {
            var requester = new Requester(string.Format("{0}{1}/{2}", Config.ApiUrl, Config.Api_Tasks_MarkTaskComplete, id), RequestMethod.POST);
            var response = requester.GetResponse();
            var statusCode = response.StatusCode;
        }

        internal static void AddLog(LogModel log)
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
        /// <summary>
        /// Gets all the URLs to crawl.
        /// </summary>
        /// <returns>A List of URLs</returns>
        public static List<string> GetCrawlerUrls()
        {
            var requester = new Requester(string.Format("{0}/r/{1}.json?limit={2}", Config.RedditBaseUrl, Config.SubReddit, Config.UrlLimit));
            var response = requester.GetResponse();
            var json = JsonConvert.DeserializeObject<RedditJSON>(response.Content);
            var urls = new List<string>();

            foreach (var item in json.data.children)
            {
                urls.Add(string.Format("{0}{1}", Config.RedditBaseUrl, item.data.permalink));
            }

            return urls;
        }

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
