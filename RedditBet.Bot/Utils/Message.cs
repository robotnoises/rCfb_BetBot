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

        public static string PrivateMsgGreet(string tempPageToken)
        {
            var msg = new Markdown(Config.MarkDown_Greetings);
            // todo, build link from token
            var link = Config.Web_ConfirmBet + tempPageToken;

            msg.ReplaceVariable("link01", MarkdownFormat.LINK("This is a link", link));

            return msg.GetText();
        }
    }
}
