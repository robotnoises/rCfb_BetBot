using System;
using Newtonsoft.Json;


namespace RedditBet.Bot.Models
{
    public class Bet
    {
        public int bet_Id { get; set; }
        public string solicitor { get; set; }
        public string challenger { get; set; }
        public string potential_challengers { get; set; }
        public string terms { get; set; }
        public int score { get; set; }
        public DateTime? event_date { get; set; }
        public DateTime? cutoff_date { get; set; }

        public Bet(string solicitor, string threadUserNames)
        {
            this.solicitor = solicitor;
            this.potential_challengers = threadUserNames;
        }
    }
}
