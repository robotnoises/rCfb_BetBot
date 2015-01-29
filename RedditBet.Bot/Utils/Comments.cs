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

        public Comment(string author, string permaLink, int upVotes)
        {
            _author = author;
            _permaLink = permaLink;
            _hashId = CreateHashId(permaLink);
            _upVotes = upVotes;
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
    /// Wrapper for RedditSharp comments (using reddit api)
    /// </summary>
    internal class ApiComment : RedditSharp.Things.Comment
    {
        private RedditSharp.Things.Comment _comment;

        public ApiComment(RedditSharp.Things.Comment comment)
        {
            _comment = (RedditSharp.Things.Comment)comment.Parent;
        }

        public BotTask ToBotTask(TaskType type)
        {
            var bt = new BotTask();
            var data = new TaskData();

            var author = _comment.Author ?? "";
            var upVotes = _comment.Upvotes.ToString() ?? "";

            bt.TaskType = type;
            bt.Completed = false;
            
            data.Add(new TaskDataItem(Config.Username_Key, author));
            data.Add(new TaskDataItem(Config.Upvotes_Key, upVotes));

            bt.TaskData = data;

            return bt;
        }
    }
}
