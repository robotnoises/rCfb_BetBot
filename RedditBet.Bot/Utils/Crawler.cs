using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using HtmlAgilityPack;
using System.Net;
using Phraze;

namespace RedditBet.Bot.Utils
{
    public class Crawler
    {
        private HtmlDocument _doc;
        private Comments _comments;

        public Crawler(string url)
        {
            _comments = new Comments();
            _doc = new HtmlDocument();

            var client = new WebClient();
            var page = WebUtility.HtmlDecode(client.DownloadString(url));

            _doc.LoadHtml(page);
        }

        public Comments GetMatchedComments(string attribute, string attributeValue, ICollection<string> keyPhrases)
        {
            var matches = new Comments();
            var phrazes = new PhraseCollection(keyPhrases);
            var nodes = _doc.DocumentNode.SelectNodes("//*[contains(concat(' ', normalize-space(@" + attribute + "), ' '), ' " + attributeValue + " ')]");
            var blackList = new HtmlNodeCollection(nodes[0]);

            // If there aren't any comments beyond the OP, there is no need to continue
            if (nodes.Count <= 1) return matches;

            // Exclude the top node, as this is the parent comment for the entire thread
            for (var i = 1; i < nodes.Count; i++)
            {
                var currentNode = nodes[i];
                var text = currentNode.InnerText.Clean();
                var locker = new object();

                if (!phrazes.HasMatch(text)) continue;
                if (blackList.Contains(currentNode)) continue;

                // Add the Comment to the match list
                matches.AddComment(Builder.Comment(currentNode));
                    
                // Add all child comments to the blacklist
                blackList = new HtmlNodeCollection(nodes[0]);
                
                Parallel.ForEach(currentNode.ChildNodes, childNode =>
                {
                    lock (locker) blackList.Add(childNode);
                });
            }

            return matches;
        }
    }
}
