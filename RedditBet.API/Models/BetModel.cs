using System;
using System.ComponentModel.DataAnnotations;

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
    }
}