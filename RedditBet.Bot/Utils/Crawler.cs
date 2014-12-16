using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using HtmlAgilityPack;
using System.Net;

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

        public Comments GetMatchedComments(string attribute, string attributeValue, Dictionary<string, double> keyWords, double confidenceFloor)
        {
            var matches = new Comments();
            var nodes = _doc.DocumentNode.SelectNodes("//*[contains(concat(' ', normalize-space(@" + attribute + "), ' '), ' " + attributeValue + " ')]");
            var topNodeXpath = nodes[1].XPath;
            var targetNodeXpath = topNodeXpath;

            // Exclude the top node, as this is the parent comment for the entire thread
            for (var i = 1; i < nodes.Count; i++)
            {
                var currentNode = nodes[i];
                var textToParse = currentNode.InnerText;
                var wordsToMatch = keyWords.Keys.ToList();
                var confidence = 0.0;

                var parser = new CommentParser(textToParse);

                if (!parser.Contains(wordsToMatch)) continue;

                foreach (var word in parser.GetMatchedWords(wordsToMatch))
                {
                    var key = word.ToLower();

                    if (keyWords.ContainsKey(key))
                    {
                        // Each keyword is assoc. w/a value
                        confidence = confidence + keyWords[key];
                        if (confidence >= 1.0) break;
                    }
                }

                if (confidence > confidenceFloor && currentNode.XPath.Length <= targetNodeXpath.Length) // Checking the length is probably dumb... todo
                {
                    var comment = Builder.Comment(currentNode, confidence);

                    targetNodeXpath = topNodeXpath;

                    if (comment != null)
                    {
                        matches.AddComment(comment);
                    }
                }
                else
                {
                    targetNodeXpath = currentNode.XPath;
                }
            }

            return matches;
        }
    }
}
