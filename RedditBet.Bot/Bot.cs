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
        private Stopwatch _timer;

        // Public methods

        public void WakeUp()
        {
            Log.Info("Bot is starting.");
            
            _timer = new Stopwatch();
            _timer.Start();
        }

        public void Sleep()
        {
            _timer.Stop();

            Log.Info(string.Format("Bot finished {0} tasks in {0}.", _tasks.Count, _timer.Elapsed));
        }

        public void AssignTasks(BotTasks tasks)
        {
            _tasks = tasks;
        }

        public void PerformAssignedTasks()
        {
            foreach (var task in _tasks)
            {
                task.Execute();
                Log.Info(string.Format("Task {0} took {1} to complete.", task.GetTaskName(), task.GetElapsedTime()));
            }
        }
    }
}
