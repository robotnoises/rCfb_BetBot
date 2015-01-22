using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedditBet.API.Models
{
    public class TempPage
    {
        [Key]
        public int TempPageId { get; set; }
        [Required]
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Visited { get; set; }
        public string Token { get; set; }

        private const int days_until_stale = 5;
        private const int numCharsForToken = 30;

        public TempPage()
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
        
        public void CreateToken()
        {
            if (!string.IsNullOrEmpty(Token))
            {
                // Todo: throw exception
            }
            else
            {
                Token = GenerateToken(numCharsForToken);
            }
        }

        // Thanks to http://stackoverflow.com/questions/730268/unique-random-string-generation#answer-8996788
        private string GenerateToken(int length, string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "length cannot be less than zero.");
            if (string.IsNullOrEmpty(allowedChars)) throw new ArgumentException("allowedChars may not be empty.");

            const int byteSize = 0x100;
            
            var hashSet = new HashSet<char>(allowedChars.ToCharArray());
            char[] allowedcharSet = new char[hashSet.Count];

            hashSet.CopyTo(allowedcharSet);

            if (byteSize < allowedcharSet.Length) throw new ArgumentException(String.Format("allowedChars may contain no more than {0} characters.", byteSize));

            // Guid.NewGuid and System.Random are not particularly random. By using a
            // cryptographically-secure random number generator, the caller is always
            // protected, regardless of use.
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                var result = new StringBuilder();
                var buf = new byte[128];
                while (result.Length < length)
                {
                    rng.GetBytes(buf);
                    for (var i = 0; i < buf.Length && result.Length < length; ++i)
                    {
                        // Divide the byte into allowedCharSet-sized groups. If the
                        // random value falls into the last group and the last group is
                        // too small to choose from the entire allowedCharSet, ignore
                        // the value in order to avoid biasing the result.
                        var outOfRangeStart = byteSize - (byteSize % allowedcharSet.Length);
                        if (outOfRangeStart <= buf[i]) continue;
                        result.Append(allowedcharSet[buf[i] % allowedcharSet.Length]);
                    }
                }
                return result.ToString();
            }
        }
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