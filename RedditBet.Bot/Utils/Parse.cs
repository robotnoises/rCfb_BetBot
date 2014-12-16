using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RedditBet.Bot.Utils
{
    
    public class CommentParser
    {
        // Private fields

        private string _textToParse;

        // Constructor

        public CommentParser(string textToParse)
        {
            _textToParse = textToParse;
        }

        // Public methods

        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="input">A list of strings, which can be single worlds or entire phrases.</param>
        /// <returns>A bool representing that the parsed text contains at least one match.</returns>
        public bool Contains(List<string> input) // int minimumMatches = 1?
        {
            var hash = ToHashSet(input);
            var r = new Regex(RegexBuilder(hash), RegexOptions.IgnoreCase);

            return r.IsMatch(_textToParse);
        }

        /// <summary>
        /// Get a list of distinct matches.
        /// </summary>
        /// <param name="input">A list of strings, which can be single worlds or entire phrases.</param>
        /// <returns>A List of matched words.</returns>
        public List<string> GetMatchedWords(List<string> input)
        {
            var hash = ToHashSet(input);
            var r = new Regex(RegexBuilder(hash), RegexOptions.IgnoreCase);
            
            // Get all matches
            var matches = r.Matches(_textToParse).Cast<Match>().Select(match => match.Value).ToList();
            // Running match list
            var matchList = new List<string>();
            // confidence score
            
            foreach (var match in matches)
            {
                if (!matchList.Contains(match)) 
                {
                    matchList.Add(match);
                }    
            }

            return matchList;
        }

        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="input">A list of strings, which can be single worlds or entire phrases.</param>
        /// <returns>An int that represents # of matches.</returns>
        public int GetCount(List<string> input)
        {
            var hash = ToHashSet(input);
            var r = new Regex(RegexBuilder(hash), RegexOptions.IgnoreCase);

            return r.Matches(_textToParse).Count;
        }

        private static string RegexBuilder(HashSet<string> input)
        {
            var sb = new StringBuilder();

            foreach (var i in input)
            {
                sb.Append("\\b" + i + "\\b|");
            }

            return sb.ToString().TrimEnd('|');
        }
        
        private static HashSet<string> ToHashSet(List<string> input)
        {
            return new HashSet<string>(input);
        }
    }
}
