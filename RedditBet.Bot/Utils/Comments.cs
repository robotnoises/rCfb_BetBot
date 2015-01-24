using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace RedditBet.Bot.Utils
{
    using HtmlAgilityPack;
    using RedditBet.Bot.Models;
    using RedditBet.Bot.DataResources;

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
        private string _message;
        private int _upVotes;
        private double _confidence;
        
        public Comment(string author, string permaLink, string message, int upVotes, double confidence)
        {
            _author = author;
            _permaLink = permaLink;
            _hashId = CreateHashId(permaLink);
            _message = message;
            _upVotes = upVotes;
            _confidence = confidence;
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

        public string GetMessage()
        {
            return _message;
        }

        public BotTask ToBotTask()
        {
            var bt = new BotTask();
            var data = new TaskData();

            data.Add(new TaskDataItem(Config.TargetUrl_Key, _permaLink));
            data.Add(new TaskDataItem(Config.HashId_Key, _hashId));
            data.Add(new TaskDataItem(Config.Message_Key, _message));

            bt.TaskType = TaskType.Reply;
            bt.TaskData = data;

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

            return permaLink.ToHashString();
        }
    }

    /// <summary>
    /// Todo
    /// </summary>
    public static class Builder
    {
        public static Comment Comment(HtmlNode node, double confidence = 1.0)
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

                // Todo: this is a temporary message
                var message = Config.MarkDown_Test;
                
                return new Comment(author.InnerText, permaLink.Attributes["href"].Value, message, uvInt, confidence);
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
