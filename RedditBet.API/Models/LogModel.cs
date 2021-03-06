﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RedditBet.API.Models
{
    public class Log
    {
        [Key]
        public int LogId { get; set; }
        public LogType Type { get; set; }
        public string Origin { get; set; }
        public string Message { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string StackTrace { get; set; }
    }

    public class LogViewModel : Mappable<LogViewModel, Log>
    {
        [Required]
        public LogType Type { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime? TimeStamp { get; set; }

        // For Exceptions, only
        public string StackTrace { get; set; }
    }
}