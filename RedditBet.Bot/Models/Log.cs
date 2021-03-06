﻿using System;

namespace RedditBet.Bot.Models
{
    public class LogModel
    {
        public LogType Type { get; set; }
        public string Origin { get { return "RedditBet.Bot"; } }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        // For Exceptions, only
        public string StackTrace { get; set; }
    }
}