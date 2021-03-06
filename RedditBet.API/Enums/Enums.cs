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
        DirectMessage,
        Monitor
    }

    [Flags]
    public enum TempPageTokenStatus
    { 
        OK,
        INVALID,
        USED,
        STALE
    }
}