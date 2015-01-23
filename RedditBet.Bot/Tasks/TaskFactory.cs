using System;
using RedditBet.Bot.Models;

namespace RedditBet.Bot.Tasks
{
    public static class TaskFactory
    {
        public static IBotTask Create(BotTask task)
        {
            switch (task.TaskType)
            { 
                case TaskType.DirectMessage:
                    return new DirectMessage(task);
                case TaskType.Reply:
                    return new Reply(task);
                case TaskType.Update:
                    return new UpdateReply(task);
                case TaskType.Monitor:
                    return new Monitor(task);
                default:
                    return new Crawl(); // Not sure if this should be the default...
            }
        }
    }
}
