using System;
using RedditBet.Bot.Enums;

namespace RedditBet.Bot.Models
{
    public class BotTask
    {
        public TaskType TaskType { get; set; }
        public string TargetUrl { get; set; }
        public string Message { get; set; }
    }
}
