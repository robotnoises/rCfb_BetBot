using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditBet.Bot.Utils
{
    // Todo: need to add real logging
    public static class Log
    {
        public static void Info(string message)
        {
            var msg = string.Format("[INFO] {0}", message);
            
            Console.WriteLine(msg);
        }

        public static void Warning(string message)
        {
            var msg = string.Format("[WARNING] {0}", message);

            Console.WriteLine(msg);
        }

        public static void Error(string message)
        {
            var msg = string.Format("[ERROR] {0}", message);

            Console.WriteLine(msg);
        }
    }
}
