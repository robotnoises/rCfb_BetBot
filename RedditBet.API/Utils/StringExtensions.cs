using System;
using System.Text;

namespace RedditBet.API
{
    using System.Security.Cryptography;

    public static class StringExtensions
    {
        public static string ToHashString(this string input)
        {
            var hash = "";
            var bytes = Encoding.UTF8.GetBytes(input);

            using (var hasher = new SHA1Managed())
            {
                try
                {
                    var h = hasher.ComputeHash(bytes);
                    var sb = new StringBuilder(h.Length * 2);

                    foreach (var b in h)
                    {
                        sb.Append(b.ToString("X2"));
                    }

                    hash = sb.ToString();
                }
                catch (Exception ex)
                {
                    // Todo rethrow StringExtenstionException
                }
            }

            return hash;
        }
    }
}