using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using HtmlAgilityPack;

namespace RedditBet.Bot.Utils
{
    public class Comments : List<Comment>
    {
        public void AddComment(Comment c)
        {
            if (this.Any(x => x.GetPermaLinkId() == c.GetPermaLinkId())) return;
            // if (this.Any(x => x.GetAuthor() == c.GetAuthor())) return; // Note: not sure if a single user can have multiple bets...
            this.Add(c);
        }
    }

    public class Comment
    {
        private string _permaLinkId;
        private string _author;
        private string _permaLink;
        private double _confidence;
        private int _upVotes;

        // Todo, need a handle on the Bot's reply url?

        public Comment(string author, string permaLink, int upVotes, double confidence)
        {
            _author = author;
            _permaLink = permaLink;
            _upVotes = upVotes;
            _confidence = confidence;
            _permaLinkId = CreatePermaLinkId(permaLink);
        }

        public string GetPermaLinkId()
        {
            return _permaLinkId;
        }

        public string GetAuthor()
        {
            return _author;
        }

        public string GetPermaLink()
        {
            return _permaLink;
        }

        public double GetConfidence()
        {
            return _confidence;
        }

        public int GetUpVotes()
        {
            return _upVotes;
        }

        /// <summary>
        /// Todo...
        /// </summary>
        /// <param name="permaLink">Takes the permaLink, hashes it, and then creates a string from it.</param>
        /// <returns>string</returns>
        private string CreatePermaLinkId(string permaLink)
        {
            /* 
             * Note: It is assumed that the permalink is guaranteed unique. This should not be used as a primary key, but given the
             * temporary nature of these records it should be safe to be used as a quick lookup value or potentially part of a url.
            */

            var id = "";
            var bytes = Encoding.UTF8.GetBytes(permaLink);

            using (var hasher = new SHA1Managed())
            {
                try
                {
                    var hash = hasher.ComputeHash(bytes);
                    var sb = new StringBuilder(hash.Length * 2);

                    foreach (var h in hash)
                    {
                        sb.Append(h.ToString("X2"));
                    }

                    id = sb.ToString();
                }
                catch (Exception ex)
                {
                    // Todo log exception
                }
            }

            return id;
        }
    }

    /// <summary>
    /// Todo
    /// </summary>
    public static class Builder
    {
        public static Comment Comment(HtmlNode node, double confidence)
        {
            var author = node.SelectSingleNode(".//a[contains(concat(' ', normalize-space(@class), ' '), ' author ')]");
            var permaLink = node.SelectSingleNode(".//a[contains(concat(' ', normalize-space(@class), ' '), ' bylink ')]");
            var upVotes = node.SelectSingleNode(".//span[contains(concat(' ', normalize-space(@class), ' '), ' score unvoted ')]");

            try
            {
                var uv = upVotes.InnerText.Split(' ')[0];
                return new Comment(author.InnerText, permaLink.Attributes["href"].Value, Convert.ToInt32(uv), confidence);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
