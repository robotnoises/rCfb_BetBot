using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedditBet.Bot.Tasks;
using RedditBet.Bot.Utils;

namespace RedditBet.Bot
{
    public class Bot
    {
        // Private fields

        private BotTasks _tasks;

        // Public methods

        public void WakeUp()
        {
            // Todo: init anything related directly to the Bot here
            Log.Info("Bot is starting.");
        }

        public void Sleep()
        {
            Log.Info("Bot has finished");
        }

        public void AssignTasks(BotTasks tasks)
        {
            _tasks = tasks;
        }

        public void PerformTasks()
        {
            // todo make Parallel?
            
            foreach (var task in _tasks)
            {
                task.Execute();
            }
        }
        
    }
}
