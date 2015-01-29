using System;
using System.Threading.Tasks;

namespace RedditBet.Bot.Tasks
{
    using RedditBet.Bot.Utils;
    using RedditBet.Bot.DataResources;

    /// <summary>
    /// Crawl is the main task, which every Bot instance will execute once
    /// </summary>
    public class Crawl : RedditTask, IBotTask
    {
        // Note: will make only 1 API call (allowed 30 per minute)

        private Comments _matchedComments;
        private const string _taskName = "Crawl";
        private object _locker = new object();

        public Crawl()
        {
            _matchedComments = new Comments();
        }

        public void Execute()
        {
            base.StartTimer();
            
            Parallel.ForEach(RedditApi.GetCrawlerUrls(), url =>
            {
                var crawler = new Scraper(url);

                var matchedComments = crawler.GetMatchedComments("class", "entry", Config.GetPhrasesToMatch());

                lock (_locker) _matchedComments.AddRange(matchedComments);
            });

            Api.AddReplyTasks(_matchedComments);
            
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
}
