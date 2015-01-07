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
            var nodes = _doc.DocumentNode.SelectNodes("//*[contains(concat(' ', normalize-space(@" + attribute + "), ' '), ' " + attributeValue + " ')]");
            var phrazes = new PhraseCollection(keyPhrases);

            // If there aren't any comments beyond the OP, there is no need to continue
            if (nodes.Count <= 1) return matches;

            // Keep track of the Xpath of the last unmatched node
            var topNodeXpath = nodes[1].XPath;
            var targetNodeXpath = topNodeXpath;
            
            // Exclude the top node, as this is the parent comment for the entire thread
            for (var i = 1; i < nodes.Count; i++)
            {
                var currentNode = nodes[i];

                //if (currentNode.XPath.Length > targetNodeXpath.Length)  // Todo: Need to do a deep comparison of Xpaths, not just a length check
                //{
                //    continue;
                //}
                //else
                //{
                //    targetNodeXpath = currentNode.XPath;
                //}

                if (phrazes.HasMatch(currentNode.InnerText))
                {
                    matches.AddComment(Builder.Comment(currentNode));
                    targetNodeXpath = topNodeXpath;
                }
            }

            return matches;
        }
    }
}
