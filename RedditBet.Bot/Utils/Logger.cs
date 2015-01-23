using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RedditBet.Bot.Utils
{
    using RedditBet.Bot.Models;
    using RedditBet.Bot.DataResources;

    public static class Log
    {
        public static void Info(string message)
        {
            var msg = string.Format("[INFO] {0}", message);
            
            Console.WriteLine(msg);

            DoLog(msg, LogType.Info);
        }

        public static void Warning(string message)
        {
            var msg = string.Format("[WARNING] {0}", message);

            Console.WriteLine(msg);

            DoLog(msg, LogType.Warning);
        }

        public static void Error(Exception ex)
        {
            var msg = string.Format("[ERROR] {0}", ex.Message);

            Console.WriteLine(msg);

            DoLog(msg, LogType.Error, ex);
        }

        private static void DoLog(string message, LogType type, Exception ex = null)
        {
            var log = new LogModel
            {
                Type = type,
                Message = message,
                StackTrace = (ex == null) ? string.Empty : ex.StackTrace,
                Timestamp = DateTime.UtcNow
            };

            Api.AddLog(log);
        }
    }
}
