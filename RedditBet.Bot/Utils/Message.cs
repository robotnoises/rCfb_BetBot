using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditBet.Bot.Utils
{
    public static class Message
    {
        public static string Test()
        {
            var header = Markdown.H2("Header");
            var anotherHeader = Markdown.H4("Here's a smaller header...");
            var para = "This is a paragraph containing information.";
            var para2 = "Hey, a second paragraph.";
            var listOfThings = Markdown.UNORDERED_LIST(new string[] { "one","2","III","FORE!" });

            return Compose(new string[] {header, anotherHeader, para, para2, listOfThings});
        }

        private static string Compose(string[] messageParts)
        {
            var sb = new StringBuilder(500);

            for (var i = 0; i < messageParts.Length; i++)
            {
                sb.Append(messageParts[i] + Markdown.LINE_BREAK);
            }

            return sb.ToString();
        }
    }
}
