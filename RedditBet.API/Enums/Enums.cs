﻿using System;

namespace RedditBet.API
{
    [Flags]
    public enum LogType
    {
        Info,
        Warning,
        Error
    }

    [Flags]
    public enum TaskType
    {
        Crawl,
        Reply,
        Update,
        DirectMessage
    }

    [Flags]
    public enum TempPageStatus
    { 
        OK,
        INVALID_TOKEN,
        STALE
    }
}