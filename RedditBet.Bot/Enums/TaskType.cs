using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditBet.Bot.Enums
{
    [Flags]
    public enum TaskType
    {
        Crawl,
        Reply,
        Update,
        DirectMessage
    }
}
