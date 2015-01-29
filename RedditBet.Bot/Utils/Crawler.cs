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

            // Todo: some logic to kill the cache
            // var foo = url.AppendRandomQueryString();

            // Todo: try catch here for 404?
            var page = WebUtility.HtmlDecode(client.DownloadString(url));

            _doc.LoadHtml(page);
        }

        public Comments GetMatchedComments(string attribute, string attributeValue, ICollection<string> keyPhrases)
        {
            var matches = new Comments();
            var phrazes = new PhraseCollection(keyPhrases);
            var blackList = new HashSet<int>();
            var nodes = _doc.DocumentNode.SelectNodes("//*[contains(concat(' ', normalize-space(@" + attribute + "), ' '), ' " + attributeValue + " ')]");

            // If there aren't any comments beyond the OP, there is no need to continue
            if (nodes.Count <= 1) return matches;

            // Exclude the top node, as this is the parent comment for the entire thread
            for (var i = 1; i < nodes.Count; i++)
            {
                var currentNode = nodes[i];
                var text = currentNode.InnerText.Clean();
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
    }
}
