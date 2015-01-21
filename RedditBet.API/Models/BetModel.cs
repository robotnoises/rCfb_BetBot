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
        [Required]
        public string Solicitor { get; set; }
        public string Challenger { get; set; }
        [Required]
        public string Terms { get; set; }
        public bool IsConfirmed { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        public DateTime? CutoffDate { get; set; }
        [ForeignKey("TempPages")]
        public int TempPageId { get; set; }
        public virtual List<TempPage> TempPages { get; set; }

        public Bet()
        {
            if (TempPages == null) TempPages = new List<TempPage>();
        }
    }
}