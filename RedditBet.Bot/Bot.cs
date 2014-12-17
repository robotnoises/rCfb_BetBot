using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedditBet.Bot.Models;

namespace RedditBet.Bot
{
    public class Bot
    {
        // Private fields

        private List<BotTask> _tasks;

        // Constructor 

        public Bot()
        {
            _tasks = new List<BotTask>();
        }

        // Public methods

        public bool WakeUp()
        {
            throw new NotImplementedException();
        }

        public void Sleep()
        {
            throw new NotImplementedException();
        }

        public void AddTasks(List<BotTask> tasks)
        {
            _tasks.AddRange(tasks);
        }
    }
}
