using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedditBet.API.Models
{
    [Table("Blacklist")]
    public class BlacklistEntry
    {
        [Key]
        public int BlacklistEntryId { get; set; }
        public string UserName { get; set; }
        public string Reason { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public class BlacklistViewModel : Mappable<BlacklistViewModel, BlacklistEntry>
    {
        [Required]
        public string UserName { get; set; }
        public string Reason { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
        
    }
}