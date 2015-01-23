using System;
using RedditBet.Bot.Utils;
using RedditBet.Bot.DataResources;
using RedditBet.Bot.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using RedditSharp;

namespace RedditBet.Bot.Tasks
{
    /// <summary>
    /// Interface for all Bot Tasks
    /// </summary>
    public interface IBotTask
    {
        void Execute();
        string GetTaskName();
        TimeSpan GetElapsedTime();
    }

    /// <summary>
    /// Todo
    /// </summary>
    public class RedditTask
    {
        protected static RedditSharp.Reddit _redditContext;
        private Stopwatch _timer;

        // Static constructor
        static RedditTask()
        {
            if (_redditContext == null)
            {
                _redditContext = RedditApi.Init(Config.Reddit_Username, Config.Reddit_Password);
            }
        }

        // Instance Constructor
        public RedditTask()
        {
            if (_timer == null)
            {
                _timer = new Stopwatch();
            }
        }

        public void StartTimer()
        {
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        public TimeSpan GetElapsed()
        {
            return _timer.Elapsed;
        }
    }
    
    /// <summary>
    /// Crawl is the main task, which every Bot instance will execute once
    /// </summary>
    public class Crawl : RedditTask, IBotTask
    {
        // Note: will make only 1 API call (allowed 30 per minute)

        private Comments _matchedComments;
        private const string _taskName = "Crawl";

        public Crawl()
        {
            _matchedComments = new Comments();
        }

        public void Execute()
        {
            base.StartTimer();
            var locker = new object();
            
            Parallel.ForEach(Data.GetCrawlerUrls(), url =>
            {
                // var url = "http://www.reddit.com/r/CFB/comments/2mvv7y/week_13_user_friendly_bet_thread/";
                var crawler = new Crawler(url);

                var matchedComments = crawler.GetMatchedComments("class", "entry", Data.GetPhrasesToMatch());

                lock (locker) _matchedComments.AddRange(matchedComments);
            });
            
            Data.SaveMatchedComments(_matchedComments);

            base.StopTimer();
        }

        public string GetTaskName()
        {
            return _taskName;
        }

        public TimeSpan GetElapsedTime()
        {
            return base.GetElapsed();
        }
    }

    /// <summary>
    /// Replies to a comment
    /// </summary>
    public class Reply : RedditTask, IBotTask
    {
        private int _taskId;
        private string _permaLink;
        private string _message;
        private string _name;
        private string _linkName;
        private const string _taskName = "Reply";

        public Reply(BotTask task)
        {
            var permaLink = task.Data.GetValue(Config.PermaLink_Key);
            var parser = new PermaLinkParser(permaLink);
            
            _taskId = task.TaskId;
            _permaLink = permaLink;
            _message = task.Message;
            _name = parser.GetNameId();
            _linkName = parser.GetLinkId();
        }

        public void Execute()
        {
            base.StartTimer();

            var user = _redditContext.GetUser(Config.Reddit_Username);
            var parentComment = _redditContext.GetComment(Config.SubReddit, _name, _linkName);

            try
            {
                var botComment = parentComment.Reply(Message.Test());

                // Mark Task as Complete
                Data.MarkCommentComplete(_taskId);

                // Todo: Log all this

            }
            catch (RateLimitException ex)
            {
                Log.Error(ex);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            base.StopTimer();
        }

        public string GetTaskName()
        {
            return _taskName;
        }

        public TimeSpan GetElapsedTime()
        {
            return base.GetElapsed();
        }
    }

    /// <summary>
    /// Updates an existing reply
    /// </summary>
    public class UpdateReply : RedditTask, IBotTask
    {
        private string _targetUrl;
        private string _message;
        private string _name;
        private string _linkName;
        private const string _taskName = "UpdateReply";

        public UpdateReply(BotTask task)
        {
            var permaLink = task.Data.GetValue(Config.PermaLink_Key);
            var parser = new PermaLinkParser(permaLink);

            _targetUrl = permaLink;
            _message = task.Message;
            _name = parser.GetNameId();
            _linkName = parser.GetLinkId();
        }

        public void Execute()
        {
            base.StartTimer();

            var comment = _redditContext.GetComment(Config.SubReddit, _name, _linkName);

            // comment.Reply(_message);

            base.StopTimer();
        }

        public string GetTaskName()
        {
            return _taskName;
        }

        public TimeSpan GetElapsedTime()
        {
            return base.GetElapsed();
        }
    }

    /// <summary>
    /// Send a direct message to a reddit user
    /// </summary>
    public class DirectMessage : RedditTask, IBotTask
    {
        private string _targetUrl;
        private string _message;
        private const string _taskName = "DirectMessage";

        public DirectMessage(BotTask task)
        {
            _targetUrl = task.Data.GetValue(Config.PermaLink_Key);
            _message = task.Message;
        }

        public void Execute()
        {
            base.StartTimer();

            // todo

            base.StopTimer();
        }

        public string GetTaskName()
        {
            return _taskName;
        }

        public TimeSpan GetElapsedTime()
        {
            return base.GetElapsed();
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
