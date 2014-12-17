using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditBet.Bot.Utils
{
    public static class Converter
    {
        public static Uri ToUri(this string url)
        {
            return new Uri(url);
        }
    }
}
