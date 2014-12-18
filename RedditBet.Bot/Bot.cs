using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using RedditBet.Bot.Tasks;
using RedditBet.Bot.Utils;

namespace RedditBet.Bot
{
    public class Bot
    {
        // Private fields

        private BotTasks _tasks;
        private Stopwatch _stopWatch;

        // Public methods

        public void WakeUp()
        {
            Log.Info("Bot is starting.");
            
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }

        public void Sleep()
        {
            Log.Info("Bot has finished");
            
            _stopWatch.Stop();

            Log.Info(string.Format("Bot finished in {0}.", _stopWatch.Elapsed));
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
