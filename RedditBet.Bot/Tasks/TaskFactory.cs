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
                default:
                    return new Crawl();
            }
        }
    }
}
