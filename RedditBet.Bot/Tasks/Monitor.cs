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
        private int _taskId;
        private string _permaLink;
        private string _message;
        private string _name;
        private const string _taskName = "Reply";
        private BotTask _task;

        public Monitor(BotTask task)
        {
            var permaLink = task.TaskData.GetValue(Config.TargetUrl_Key);
            var parser = new PermaLinkParser(permaLink);

            _taskId = task.TaskId;
            _permaLink = permaLink;
            _message = task.TaskData.GetValue(Config.Message_Key);
            _name = parser.GetNameId();
            _task = task;
        }
        public void Execute()
        {
            base.StartTimer();

            var crawler = new Crawler(_permaLink);
            var foundMatch = false;

            // Check to see if user declines
            var declineComments = crawler.GetMatchedComments("class", "entry", Config.GetDeclinePhrases());
            if (declineComments.Count > 0 && FoundMatchForUser(declineComments))
            { 
                // Add a new Task to Update the Original Bot reply (apoligize, etc etc)
                foundMatch = true;
            }

            // Check to see if user unsubscribes
            var unsubscribeComments = crawler.GetMatchedComments("class", "entry", Config.GetUnsubscribePhrases());
            if (!foundMatch && unsubscribeComments.Count > 0 && FoundMatchForUser(unsubscribeComments))
            {
                // make a call to API to add user to the blacklist
                foundMatch = true;
            }

            // Check to see if user confirms
            var confirmComments = crawler.GetMatchedComments("class", "entry", Config.GetConfirmationPhrases());
            if (!foundMatch && confirmComments.Count > 0 && FoundMatchForUser(confirmComments))
            {
                // make a call to API to add a new bet record
                // Add a new Task to send a DM to the OP
                // Add a new Task to Update the Original Bot reply
                if (confirmComments.Any(x => x.GetAuthor() == _task.TaskData.GetValue(Config.Username_Key)))
                {
                    foundMatch = true;
                }
            }

            if (foundMatch)
            {
                // Api.MarkTaskComplete(_taskId);
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

        private bool FoundMatchForUser(Comments matchedComments)
        { 
            // Iterate through matched comments and confirm the username matches the OP

            // temp
            return matchedComments.Count > 0;
        }
    }
}
