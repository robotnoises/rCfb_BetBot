using System;
using RedditBet.Bot.Enums;
using RedditBet.Bot.Utils;
using RedditBet.Bot.DataResources;
using RedditBet.Bot.Models;
using System.Collections.Generic;
using RedditSharp;

namespace RedditBet.Bot.Tasks
{
    /// <summary>
    /// Interface for all Bot Tasks
    /// </summary>
    public interface IBotTask
    {
        void Execute();
    }

    /// <summary>
    /// Static RedditSharp context to be used for each Task instance
    /// </summary>
    public class RedditBotTask
    {
        protected static RedditSharp.Reddit _redditContext;

        static RedditBotTask()
        {
            if (_redditContext == null)
            {
                _redditContext = RedditApi.Init(Config.Reddit_Username, Config.Reddit_Password);
            }
        }
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

            //foreach (var url in Data.GetCrawlerUrls())
            //{
                var url = "http://rc.reddit.com/r/CFB/comments/1rkt6s/week_14_user_friendly_bet_thread/";
                var crawler = new Crawler(url);

                var matches = crawler.GetMatchedComments("class", "entry", Data.GetPhrasesToMatch());    
                
                // todo, probably shouldn't log this to the db
                Log.Info(string.Format("Found {0} matches in {1}", matches.Count, url));

                _matchedComments.AddRange(matches);
            //}

            Data.SaveComment(_matchedComments);
        }
    }

    /// <summary>
    /// Replies to a comment
    /// </summary>
    public class Reply : RedditBotTask, IBotTask
    {
        private string _permaLink;
        private string _message;
        private string _name;
        private string _linkName;

        public Reply(BotTask task)
        {
            var parser = new PermaLinkParser(task.TargetUrl);
            
            _permaLink = task.TargetUrl;
            _message = task.Message;
            _name = parser.GetNameId();
            _linkName = parser.GetLinkId();
        }

        public void Execute()
        {
            var user = _redditContext.GetUser(Config.Reddit_Username);
            var comment = _redditContext.GetComment(Config.SubReddit, _name, _linkName);

            try
            {
                var botComment = comment.Reply(Message.Test());
            }
            catch (RateLimitException ex)
            {
                // Todo log?
                var foo = ex.TimeToReset;
            }
            catch (Exception ex)
            { 
            
            }
            
            // Todo, mark task as complete
            // Data.
        }
    }

    /// <summary>
    /// Updates an existing reply
    /// </summary>
    public class UpdateReply : RedditBotTask, IBotTask
    {
        private string _targetUrl;
        private string _message;
        private string _name;
        private string _linkName;

        public UpdateReply(BotTask task)
        {
            var parser = new PermaLinkParser(task.TargetUrl);

            _targetUrl = task.TargetUrl;
            _message = task.Message;
            _name = parser.GetNameId();
            _linkName = parser.GetLinkId();
        }

        public void Execute()
        {
            var comment = _redditContext.GetComment(Config.SubReddit, _name, _linkName);

            // comment.Reply(_message);
        }
    }

    /// <summary>
    /// Send a direct message to a reddit user
    /// </summary>
    public class DirectMessage : IBotTask
    {
        private string _targetUrl;
        private string _message;

        public DirectMessage(BotTask task)
        {
            _targetUrl = task.TargetUrl;
            _message = task.Message;
        }

        public void Execute()
        {
            // todo
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
            // this.AddRange(Data.GetIncompleteTasks());
        }
    }
}
