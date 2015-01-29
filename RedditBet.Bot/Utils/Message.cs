using System;
using System.Collections.Generic;

namespace RedditBet.Bot.Utils
{
    public static class Message
    {
        public static string Test()
        {
            var msg = new Markdown(Config.MarkDown_Test);

            msg.ReplaceVariable("link01", MarkdownFormat.LINK("This is a link to google", "http://www.google.com"));

            return msg.GetText();
        }
    }
}
