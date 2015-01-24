using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditBet.Bot
{
    [Flags]
    public enum RequestMethod
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}
