using System;
using RedditBet.Bot.Enums;
using RedditBet.Bot.Utils;
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
        private Comments _matchedComments;

        public Crawl()
        {
            _matchedComments = new Comments();
        }

        public void Execute()
        {
            foreach (var url in Config.GetCrawlerUrls())
            {
                Log.Info("Fetching URLs.");

                var crawler = new Crawler(url);
                var matches = crawler.GetMatchedComments("class", "entry", Config.GetTargetWords(), 0.7);

                Log.Info(string.Format("Found {0} matches in {1}", matches.Count, url));

                _matchedComments.AddRange(matches);
            }
        }
    }

    //public class Reply : IBotTask
    //{
    //    private int _taskId;
    //    private TaskType _taskType;
    //    private DateTime _timeAssigned;
    //    private DateTime? _timeCompleted;
    //    private string _targetUrl;
    //    private bool _completed;
    //    private string _message;

    //    public void Execute()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class UpdateReply : IBotTask
    //{
    //    private int _taskId;
    //    private TaskType _taskType;
    //    private DateTime _timeAssigned;
    //    private DateTime? _timeCompleted;
    //    private string _targetUrl;
    //    private bool _completed;
    //    private string _message;

    //    public void Execute()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class DirectMessage : IBotTask
    //{
    //    private int _taskId;
    //    private TaskType _taskType;
    //    private DateTime _timeAssigned;
    //    private DateTime? _timeCompleted;
    //    private string _targetUrl;
    //    private bool _completed;
    //    private string _message;

    //    public void Execute()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class BotTasks : List<IBotTask>
    {
        public BotTasks()
        {
            // Crawl is always run once
            this.Add(new Crawl());
        }

        /// <summary>
        /// Will fetch only incomplete Tasks
        /// </summary>
        public void Fetch()
        { 
            // Todo: grab tasks from database (API)
        }
    }
}
