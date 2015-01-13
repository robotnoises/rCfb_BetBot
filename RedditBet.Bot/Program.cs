using System;
using RedditBet.Bot.Tasks;

namespace RedditBet.Bot
{
    class Program
    {
        static void Main()
        {
            var robot = new Bot();
            var tasks = new BotTasks();

            // Load tasks to be carried-out by the bot (incomplete)
            tasks.Load();

            // Init Bot
            robot.WakeUp();

            // Assign the tasks
            robot.AssignTasks(tasks);

            // Perform each task
            robot.PerformAssignedTasks();

            // Night-night
            robot.Sleep();
        }
    }
}

/*
 Odd behavior:
 * 
 * http://www.reddit.com/r/CFB/comments/2s7twt/pregame_thread_national_championship_oregon_vs/cnn08ov
 * 
 * Appears to be matching the lowest in the tree rather than the highest. Check crawler blacklist logic.
 */
