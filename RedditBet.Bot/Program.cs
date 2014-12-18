using System;
using RedditBet.Bot.Tasks;

namespace RedditBet.Bot
{
    class Program
    {
        /* Bot stuff: */

        // Todo: Bot (Crawling) need to be able to get the "thing" id for replies 
        // Todo: Bot needs to be able to pick up "Tasks" beyond just searching comments
        // Todo: Bot needs to be able to send DMs
        // Todo: Bot needs to be able to update replies
        // Todo: Bot needs to accept arguments to either Crawl or pick up tasks from the API
        // Todo: Bot needs a TaskFactory to carry-out various tasks via a common task interface
        // Todo: Bot needs to be able to check if logged into reddit, and log in
        // Todo: Bot needs to time itself

        /* Other stuff: */

        // Add Tables for Comments (user), Tasks (for bot to carry out), Replies (Bot-only replies)
        // Add Logging
        // Add new Web project to serve (initially) as the place where users confirm bets

        // Note: Need to add a global tracker to keep a running count of how many Reddit API calls there are in a given run...
        // ... there is rate-limiting in play (30/60 per minute for unauth/auth)
        
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
            robot.PerformTasks();

            // Night-night
            robot.Sleep();
        }
    }
}

