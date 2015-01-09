using System;

namespace RedditBet.API
{
    [Flags]
    public enum LogType
    {
        Info,
        Warning,
        Error
    }
}