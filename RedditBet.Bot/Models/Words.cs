using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditBet.Bot.Models
{
    public class Words
    {
        public string title { get; set; }
        public string description { get; set; }
        public Dictionary<string, double> words { get; set; }
    }

    public class Phrases
    {
        public string title { get; set; }
        public string description { get; set; }
        public ICollection<string> phrases { get; set; }
    }
}
