using System;
using System.Collections.Generic;

namespace RedditBet.Bot.Utils
{
    using RedditBet.Bot.Models;

    internal static class Builder
    {
        #region Bot Tasks

        public static BotTask MonitorTask()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Crawler

        public static Comment Comment(HtmlAgilityPack.HtmlNode node, double confidence = 1.0)
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
                var message = Message.Test();

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

        #endregion
    }
}
