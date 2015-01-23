using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedditBet.API.Models
{
    [Table("TempPages")]
    public class TempPageData
    {
        [Key]
        public int TempPageId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Visited { get; set; }
        public string Token { get; set; }

        private const int days_until_stale = 5;
        private const int numCharsForToken = 30;

        public TempPageData()
        {
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// "Fresh" tokens have not yet been used and wwere created within the last x-days
        /// </summary>
        /// <returns>bool</returns>
        public bool IsFresh()
        {
            var timeSpan = DateTime.UtcNow - CreatedAt;
            var daysOld = (int)Math.Ceiling(timeSpan.TotalDays);

            return (!Visited && daysOld <= days_until_stale);
        }
        
        public bool HasToken()
        {
            return !string.IsNullOrEmpty(Token);
        }
    }

    public class TempPageDataViewModel : Mappable<TempPageDataViewModel, TempPageData>
    {
        [Required]
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Visited { get; set; }
    }

    public class TokenValidationResponse
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }

        private const string _validMsg = "This token is valid.";
        private const string _invalidMsg = "This token is invalid.";
        private const string _usedMsg = "This token has already been used.";
        private const string _staleMsg = "This token is too old to be valid.";

        public TokenValidationResponse(TempPageTokenStatus status)
        {
            IsValid = (status == TempPageTokenStatus.OK);
            Message = GetMessage(status);
        }

        private string GetMessage(TempPageTokenStatus status)
        {
            switch (status)
            { 
                case TempPageTokenStatus.OK:
                    return _validMsg;
                case TempPageTokenStatus.INVALID:
                    return _invalidMsg;
                case TempPageTokenStatus.USED:
                    return _usedMsg;
                case TempPageTokenStatus.STALE:
                    return _staleMsg;
                default:
                    return _invalidMsg;
            }
        }
    }
}