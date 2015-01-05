using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RedditBet.Bot.Utils
{
    public static class MarkdownFormat
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
            }

            return sb.ToString();
        }
        
        public static string LINK(string text, string url)
        {
            return string.Format("[{0}]({1})", text, url);
        }
    }

    public class Markdown
    {
        private string _markdown;
        private List<string> _variables;

        public Markdown(string markdown)
        {
            _markdown = markdown;
            _variables = new List<string>();
            _variables.AddRange(GetVariables());

            //var variables = GetVariables();

            //if (variables.Count() > 0)
            //{
            //    _variables.AddRange(variables);
            //}
        }

        public void ReplaceVariable(string variableName, string replacement)
        {
            var variable = "%" + variableName + "%";

            if (_variables.Contains(variableName))
            {
                _markdown = _markdown.Replace(variable, replacement);
            }
            else
            { 
                // TODO: Throw 'varible not found' Exception
            }
        }

        public string GetText()
        {
            return _markdown;
        }

        private IEnumerable<string> GetVariables()
        {
            var regex = new Regex(@"%[a-z0-9]+%", RegexOptions.IgnoreCase);
            var matches = regex.Matches(_markdown, 0).Cast<Match>().Select(x => x.Value).ToList();
            var variableNames = new List<string>();

            foreach (var match in matches)
            { 
                variableNames.Add(match.Replace("%", ""));
            }

            return variableNames;
        }
    }
}
