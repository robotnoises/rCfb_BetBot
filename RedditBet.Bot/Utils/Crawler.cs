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

            // If there aren't any comments beyond the OP, there is no need to continue
            if (nodes.Count <= 1) return matches;
            
            // Keep track of the Xpath of the last unmatched node
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

                // Break-out if nothing is matched
                if (!parser.Contains(wordsToMatch)) continue;

                foreach (var word in parser.GetMatchedWords(wordsToMatch))
                {
                    var key = word.ToLower();

                    if (ContainsKeyWithSpaces(keyWords, key))
                    {
                        // Each keyword is assoc. w/a value
                        confidence = confidence + GetValueForKeyWithSpaces(keyWords, key);
                        if (confidence >= 1.0) break;
                    }
                }

                // Checking the length is probably dumb... todo
                if (confidence > confidenceFloor && currentNode.XPath.Length <= targetNodeXpath.Length) 
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

        /// <summary>
        /// I'm really abusing the Dictionary class at this point...
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool ContainsKeyWithSpaces(Dictionary<string, double> dict, string key)
        {
            var found = false;
            var keyHashSet = new HashSet<string>(dict.Keys);

            foreach (var k in dict.Keys)
            {
                if (key == k.ToLower())
                {
                    found = true;
                    break;
                }
            }

            return found;
        }

        /// <summary>
        /// Gruhhhhh
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private double GetValueForKeyWithSpaces(Dictionary<string, double> dict, string key)
        {
            var value = 0.0;
            var count = 0;
            var values = dict.Values.ToArray();

            if (key.Contains(' '))
            {
                foreach (var k in dict.Keys)
                {
                    if (key == k.ToLower())
                    {
                        value = values[count];
                        break;
                    }

                    count++;
                }
            }
            else
            {
                value = dict[key];
            }

            return value;
        }
    }
}
