using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditBet.Bot.Utils
{
    public static class Markdown
    {
        public static string LINE_BREAK = "\n\n";

        public static string H1 (string text) {
            return string.Format("#{0}", text);
        }
        
        public static string H2(string text)
        {
            return string.Format("##{0}", text);
        }

        public static string H3(string text)
        {
            return string.Format("###{0}", text);
        }

        public static string H4(string text)
        {
            return string.Format("####{0}", text);
        }

        public static string H5(string text)
        {
            return string.Format("#####{0}", text);
        }

        public static string UNORDERED_LIST(string[] listItems)
        {
            var sb = new StringBuilder(500);

            foreach (var item in listItems)
            {
                sb.Append(string.Format("* {0}{1}", item, LINE_BREAK));
                // sb.AppendLine(string.Format("* {0}", item));
            }

            return sb.ToString();
        }
        
        public static string LINK(string text, string url)
        {
            return string.Format("[{0}]({1})", text, url);
        }
    }
}
