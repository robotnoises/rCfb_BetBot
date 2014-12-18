using System;
using RedditBet.Bot.Enums;

namespace RedditBet.Bot.Models
{
    public class BotTask
    {
        //public int TaskId { get; set; }
        public TaskType TaskType { get; set; }
        //public DateTime TimeAssigned { get; set; }
        //public DateTime? TimeCompleted { get; set; }
        //public bool Completed { get; set; }
        public string TargetUrl { get; set; }
        public string Message { get; set; }
    }
}
