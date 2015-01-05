using System;
using System.Collections.Generic;
using System.Text;
using RedditBet.Bot.DataResources;

namespace RedditBet.Bot.Utils
{
    public static class Message
    {
        public static string Test()
        {
            var msg = new Markdown(Data.MarkDown_Test);

            msg.ReplaceVariable("link01", MarkdownFormat.LINK("This is a link to google", "http://www.google.com"));

            return msg.GetText();
        }
    }
}
