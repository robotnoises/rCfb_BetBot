using System;
using System.ComponentModel.DataAnnotations;

namespace RedditBet.API.Models
{
    public class BlacklistEntry
    {
        [Key]
        public int BlacklistEntryId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
        public string Reason { get; set; }
    }
}