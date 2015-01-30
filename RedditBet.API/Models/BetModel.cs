using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedditBet.API.Models
{
    public class Bet
    {
        [Key]
        public int BetId { get; set; }
        public string Solicitor { get; set; }
        public string Challenger { get; set; }
        public string Terms { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime? CutoffDate { get; set; }
        public bool IsConfirmed { get; set; }
        public int Score { get; set; }
        
        public virtual List<TempPageData> TempPages { get; set; }
        public virtual Fulfillment Fulfillment { get; set; }

        public bool IsFullfilled()
        {
            if (Fulfillment == null) return false;

            return Fulfillment.IsFulfilled;
        }
    }

    public class BetViewModel : Mappable<BetViewModel, Bet>
    {
        public int BetId { get; set; }
        [Required]
        public string Solicitor { get; set; }
        public string Challenger { get; set; }
        [Required]
        public string Terms { get; set; }
        public int Score { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        public DateTime? CutoffDate { get; set; }
        public List<TempPageData> TempPages { get; set; }
    }

    public class BetCollection
    {
        [JsonProperty("bets")]
        public IEnumerable<Bet> Bets { get; set; }

        public BetCollection(IEnumerable<Bet> bets)
        {
            this.Bets = bets;
        }
    }


}