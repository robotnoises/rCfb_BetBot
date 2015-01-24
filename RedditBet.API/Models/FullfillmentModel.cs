using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedditBet.API.Models
{
    public class Fulfillment
    {
        [Key, ForeignKey("Bet")]
        public int BetId { get; set; }
        public bool IsFulfilled { get; set; }
        public string LoserStatement { get; set; }

        public virtual List<RelevantLink> RelevantLinks { get; set; }
        public virtual Bet Bet { get; set; }

        public bool IdValidFulfillment()
        { 
            return (IsFulfilled && (!string.IsNullOrEmpty(LoserStatement) || RelevantLinks.Count > 0));
        }
    }

    public class FulfillmentViewModel : Mappable<FulfillmentViewModel, Fulfillment>
    {
        public int BetId { get; set; }
        public bool IsFulfilled { get; set; }
        public string LoserStatement { get; set; }
        public virtual List<RelevantLink> RelevantLinks { get; set; }
    }

    public class RelevantLink
    {
        [Key]
        public int RelevantLinkId { get; set; }
        public string LinkText { get; set; }
        public string Url { get; set; }
    }
}