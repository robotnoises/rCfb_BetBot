﻿using System;
using RedditBet.Bot.Enums;
using RedditBet.Bot.Utils;
using RedditBet.Bot.DataHelpers;
using RedditBet.Bot.Models;
using System.Collections.Generic;

namespace RedditBet.Bot.Tasks
{
    public interface IBotTask
    {
        void Execute();
    }
    
    /// <summary>
    /// Crawl is the main task, which every Bot instance will execute once
    /// </summary>
    public class Crawl : IBotTask
    {
        // Note: will make only 1 API call (allowed 30 per minute)

        private Comments _matchedComments;

        public Crawl()
        {
            _matchedComments = new Comments();
        }

        public void Execute()
        {
            Log.Info("Fetching URLs.");

            foreach (var url in Data.GetCrawlerUrls())
            {
                var crawler = new Crawler(url);
                var matches = crawler.GetMatchedComments("class", "entry", Data.GetMatchWords(), 0.7);

                // todo, probably shouldn't og this to the db
                Log.Info(string.Format("Found {0} matches in {1}", matches.Count, url));

                _matchedComments.AddRange(matches);
            }
        }
    }

    /// <summary>
    /// Replies to a comment
    /// </summary>
    public class Reply : IBotTask
    {
        private string _targetUrl;
        private bool _completed;
        private string _message;

        public Reply(BotTask task)
        {
            _targetUrl = task.TargetUrl;
            _completed = false;
            _message = task.Message;
        }

        public void Execute()
        {
            // throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Updates an existing reply
    /// </summary>
    public class UpdateReply : IBotTask
    {
        private string _targetUrl;
        private bool _completed;
        private string _message;

        public UpdateReply(BotTask task)
        {
            _targetUrl = task.TargetUrl;
            _completed = false;
            _message = task.Message;
        }

        public void Execute()
        {
            // throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Sends a direct message to a reddit user
    /// </summary>
    public class DirectMessage : IBotTask
    {
        private string _targetUrl;
        private bool _completed;
        private string _message;

        public DirectMessage(BotTask task)
        {
            _targetUrl = task.TargetUrl;
            _completed = false;
            _message = task.Message;
        }

        public void Execute()
        {
            // throw new NotImplementedException();
        }
    }

    /// <summary>
    /// A list of BotTasks
    /// </summary>
    public class BotTasks : List<IBotTask>
    {
        public BotTasks()
        {
            // Crawl is always run once
            this.Add(new Crawl());
        }

        /// <summary>
        /// Will fetch only incomplete Tasks (can't think of a good reason for it to get anything but incomplete...)
        /// </summary>
        public void Load()
        {
            this.AddRange(Data.GetIncompleteTasks());
        }
    }
}