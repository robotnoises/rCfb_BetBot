using System;
using System.Net;
using System.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RedditBet.Bot.Utils
{
    using Phraze;
    using HtmlAgilityPack;

    public class Scraper
    {
        private string _url;
        private HtmlDocument _doc;
        private Comments _comments;

        public Scraper(string url, bool clearCache = true)
        {
            var client = new WebClient();

            _comments = new Comments();
            _doc = new HtmlDocument();
            _url = clearCache ? url.AppendRandomQueryString() : url;

            try
            {
                _doc.LoadHtml(WebUtility.HtmlDecode(client.DownloadString(_url)));
            }
            catch (Exception ex)
            {
                // Todo: try catch here for 404?
            }
        }

        public Comments GetMatchedComments(string attribute, string attributeValue, ICollection<string> keyPhrases)
        {
            var matches = new Comments();
            var phrazes = new PhraseCollection(keyPhrases);
            var blackList = new HashSet<int>();
            var nodes = _doc.DocumentNode.SelectNodes("//*[contains(concat(' ', normalize-space(@" + attribute + "), ' '), ' " + attributeValue + " ')]");

            // If there aren't any comments beyond the OP, there is no need to continue
            if (nodes == null || nodes.Count <= 1) return matches;

            // Exclude the top node, as this is the parent comment for the entire thread
            for (var i = 1; i < nodes.Count; i++)
            {
                var currentNode = nodes[i];
                var text = currentNode.InnerText.ScrubRedditComment();
                var locker = new object();

                if (!phrazes.HasMatch(text)) continue;
                if (blackList.Contains(currentNode.StreamPosition)) continue;

                var comment = Builder.Comment(currentNode);

                // Add the Comment to the match list
                matches.AddComment(comment);

                var matchedPhrases = String.Join(" | ", phrazes.Matches(text));

                Log.Info(string.Format("Matched Comment ({0}) (matched phrase(s): {1})", comment.GetHashId(), matchedPhrases));

                // Add all child comments to the blacklist
                Parallel.ForEach(currentNode.ChildNodes, childNode =>
                {
                    lock (locker) blackList.Add(childNode.StreamPosition);
                });
            }

            return matches;
        }

        public ICollection<string> GetAll(string attribute, string attributeValue)
        {
            var items = new HashSet<string>();
            var locker = new object();

            var nodes = _doc.DocumentNode.SelectNodes("//*[contains(concat(' ', normalize-space(@" + attribute + "), ' '), ' " + attributeValue + " ')]");

            Parallel.ForEach(nodes, node => {
                lock (locker) items.Add(node.InnerText.ScrubRedditComment());
            });

            return items;
        }
    }
}
