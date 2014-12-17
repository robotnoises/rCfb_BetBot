﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RedditBet.API.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        public int TaskType { get; set; }
        public DateTime TimeAssigned { get; set; }
        public DateTime? TimeCompleted { get; set; }
        public bool Completed { get; set; }
        public string TargetUrl { get; set; }
        public string Message { get; set; }
    }
}