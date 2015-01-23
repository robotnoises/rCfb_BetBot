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
            this.AddRange(Api.GetIncompleteTasks());
        }
    }
}
