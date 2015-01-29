using System;
using System.Text;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace RedditBet.API.Services
{
    using RedditBet.API.Models;
    using RedditBet.API.Repositories;
    
    
    internal class TempPageService
    {
        private IRepository<TempPageData> _repo;

        public TempPageService()
        {
            _repo = new Repository<TempPageData>(DatabaseContext.Create());
        }

        public TempPageService(DbContext context)
        {
            _repo = new Repository<TempPageData>(context);
        }

        public TempPageTokenStatus ValidateToken(string token)
        {
            // Todo: need to log these statuses 

            if (string.IsNullOrEmpty(token)) return TempPageTokenStatus.INVALID;

            var result = _repo.GetWhere(x => x.Token == token);
            
            if (result.Count() > 1) return TempPageTokenStatus.INVALID;
            
            var tempPage = result.FirstOrDefault();
                       
            if (tempPage == null)                           return TempPageTokenStatus.INVALID;
            if (!tempPage.Visited && tempPage.IsFresh())    return TempPageTokenStatus.OK;
            if (tempPage.Visited)                           return TempPageTokenStatus.USED;
            if (!tempPage.IsFresh())                        return TempPageTokenStatus.STALE;
            else                                            return TempPageTokenStatus.INVALID;
        }

        // Thanks to http://stackoverflow.com/questions/730268/unique-random-string-generation#answer-8996788
        public string GenerateToken(int tokenSize = 30)
        {
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            if (tokenSize < 0) throw new ArgumentOutOfRangeException("tokenSize", "length cannot be less than zero.");
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
                while (result.Length < tokenSize)
                {
                    rng.GetBytes(buf);
                    for (var i = 0; i < buf.Length && result.Length < tokenSize; ++i)
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
}