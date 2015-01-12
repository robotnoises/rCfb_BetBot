using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using HtmlAgilityPack;
using RedditBet.Bot.Models;

namespace RedditBet.Bot.Utils
{
    public class Comments : List<Comment>
    {
        public void AddComment(Comment c)
        {
            if (c == null) return;
            if (this.Any(x => x.GetHashId() == c.GetHashId())) return;
            
            this.Add(c);
        }
    }

    public class Comment
    {
        private string _author;
        private string _permaLink;
        private string _hashId;
        private int _upVotes;
        private double _confidence;
        private ICollection<string> _matchedPhrases;

        public Comment(string author, string permaLink, int upVotes, double confidence, ICollection<string> matchedPhrases)
        {
            _author = author;
            _permaLink = permaLink;
            _hashId = CreateHashId(permaLink);
            _upVotes = upVotes;
            _confidence = confidence;
            _matchedPhrases = matchedPhrases;
        }

        public string GetHashId()
        {
            return _hashId;
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

        public BotTask ToBotTask(Enums.TaskType taskType = Enums.TaskType.Reply)
        {
            var bt = new BotTask();

            bt.TaskType = taskType;
            bt.TargetUrl = _permaLink;
            bt.HashId = _hashId;
            bt.Message = ""; // Todo, create msg based on TaskType and confidence

            return bt;
        }

        /// <summary>
        /// Todo...
        /// </summary>
        /// <param name="permaLink">Takes the permaLink, hashes it, and then creates a string from it.</param>
        /// <returns>string</returns>
        private string CreateHashId(string permaLink)
        {
            /* 
             * Note: It is assumed that the permalink is guaranteed unique. This should not be used as a primary key, but given the
             * temporary nature of these records it should be safe to be used as a key for quick lookups or potentially part of a temp url.
            */

            var id = "";
            var bytes = Encoding.UTF8.GetBytes(permaLink);

            using (var hasher = new SHA1Managed())
            {
                try
                {
                    var hash = hasher.ComputeHash(bytes);
                    var sb = new StringBuilder(hash.Length * 2);

                    foreach (var b in hash)
                    {
                        sb.Append(b.ToString("X2"));
                    }

                    id = sb.ToString();
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
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
        public static Comment Comment(HtmlNode node, ICollection<string> matchedPhrases, double confidence = 1.0)
        {
            var author = node.SelectSingleNode(BuildSelector("a", "class", "author"));
            var permaLink = node.SelectSingleNode(BuildSelector("a", "class", "bylink"));
            var upVotes = node.SelectSingleNode(BuildSelector("span", "class", "score unvoted"));

            try
            {
                // Originally captured as "### points", so chop-off the " points"
                var uv = upVotes.InnerText.Split(' ')[0];
                
                // ... and Convert to int
                var uvInt = Convert.ToInt32(uv);
                
                return new Comment(author.InnerText, permaLink.Attributes["href"].Value, uvInt, confidence, matchedPhrases);
            }
            catch (Exception ex)
            {
                Log.Error(ex);

                return null;
            }
        }

        private static string BuildSelector(string element, string attribute, string value)
        {
            return string.Format(".//{0}[contains(concat(' ', normalize-space(@{1}), ' '), ' {2} ')]", element, attribute, value);
        }
    }
}
