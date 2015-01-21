using System;
using System.ComponentModel.DataAnnotations;

namespace RedditBet.API.Models
{
    internal class TempPage
    {
        [Key]
        public int TempPageId { get; set; }
        [Required]
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Visited { get; set; }
        [Required]
        public virtual Bet AssociatedBet { get; set; }

        private const int days_until_stale = 5;

        public bool IsFresh()
        {
            return (!Visited && CreatedAt.AddDays(days_until_stale) <= DateTime.UtcNow);
        }

    }
}