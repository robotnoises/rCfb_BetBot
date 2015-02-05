using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace RedditBet.API.Models
{
    public class Bet : Mappable<Bet, BetViewModel>
    {
        [Key]
        public int BetId { get; set; }
        public string Solicitor { get; set; }
        public string Challenger { get; set; }
        public string Terms { get; set; }
        public string PotentialChallengers { get; set; }
        public DateTime? EventDate { get; set; }
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
        [JsonProperty("bet_id")]
        public int BetId { get; set; }
        [Required, JsonProperty("solicitor")]
        public string Solicitor { get; set; }
        [JsonProperty("challenger")]
        public string Challenger { get; set; }
        [JsonProperty("potential_challengers")]
        public string PotentialChallengers { get; set; }
        [JsonProperty("terms")]
        public string Terms { get; set; }
        [JsonProperty("score")]
        public int Score { get; set; }
        [JsonProperty("event_date")]
        public DateTime? EventDate { get; set; }
        [JsonProperty("cutoff_data")]
        public DateTime? CutoffDate { get; set; }
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

    public class BetCreationResponse
    {
        [JsonProperty("bet_id")]
        public int BetId { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }

        public BetCreationResponse(Bet b)
        {
            BetId = b.BetId;
            Token = b.TempPages.Where(x => x.IsFresh()).Select(x => x.Token).FirstOrDefault();
        }
    }
}