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
        public bool IsConfirmed { get; set; }
        public int Score { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime? CutoffDate { get; set; }
                
        public virtual List<TempPageData> TempPages { get; set; }
        public Fulfillment FulfillmentData { get; set; }

        public bool IsFullfilled()
        {
            if (FulfillmentData == null) return false;

            return FulfillmentData.IsFulfilled;
        }
    }

    public class BetViewModel : Mappable<BetViewModel, Bet>
    {
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
}