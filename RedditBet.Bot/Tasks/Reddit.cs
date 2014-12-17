using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditBet.Bot.Tasks
{
    public class RedditJSON
    {
        public string kind { get; set; }
        public RedditPage data { get; set; }
    }

    public class RedditPage
    {
        public string modhash { get; set; }
        public List<RedditPosts> children { get; set; }
        public string after { get; set; }
        public string before { get; set; }
    }

    public class RedditPosts
    {
        public string kind { get; set; }
        public RedditPostData data { get; set; }
    }

    public class RedditPostData
    {
        public string permalink { get; set; }
    }
}
