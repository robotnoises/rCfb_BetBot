using System;
using System.Linq;

namespace RedditBet.Bot.Tasks
{
    using RedditBet.Bot.DataResources;
    using RedditBet.Bot.Models;
    using RedditBet.Bot.Utils;

    // Info this task requres:

    // username of the OP, i.e. the comment that the bot replied directly to
    // a permalink, to know where to monitor
    // the task id, to update the task or mark it complete

    class Monitor : RedditTask, IBotTask
    {
        private BotTask _task;
        private string _permaLink;
        private const string _taskName = "Reply";

        public Monitor(BotTask task)
        {
            _permaLink = task.TaskData.GetValue(Config.TargetUrl_Key);
            
            _task = task;
        }
        public void Execute()
        {
            base.StartTimer();

            var crawler = new Crawler(_permaLink);
            var foundMatch = false;

            // Check to see if user declines
            if (FoundMatchForOP(crawler.GetMatchedComments("class", "entry", Config.GetDeclinePhrases())))
            { 
                // Add a new Task to Update the Original Bot reply (apoligize, etc)
                foundMatch = true;
            }

            // Check to see if user unsubscribes
            if (!foundMatch && FoundMatchForOP(crawler.GetMatchedComments("class", "entry", Config.GetUnsubscribePhrases())))
            {
                // make a call to API to add user to the blacklist
                foundMatch = true;
            }

            // Check to see if user confirms
            if (!foundMatch && FoundMatchForOP(crawler.GetMatchedComments("class", "entry", Config.GetConfirmationPhrases())))
            {
                // make a call to API to add a new bet record
                // Add a new Task to send a DM to the OP
                // Add a new Task to Update the Original Bot reply
                
                foundMatch = true;
            }

            if (foundMatch)
            {
                Api.MarkTaskComplete(_task.TaskId);
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

        private bool FoundMatchForOP(Comments matchedComments)
        { 
            // Iterate through matched comments and confirm the username of the top matched comment matches the OPs username
            var topMatch = matchedComments
                .Where(x => x.GetAuthor() == _task.TaskData.GetValue(Config.Username_Key))
                .OrderByDescending(x => x.GetUpVotes())
                .FirstOrDefault();
            
            // Did we get a top match?
            return topMatch != null;
        }
    }
}
