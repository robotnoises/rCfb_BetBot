using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RedditBet.Bot.Utils
{
    public static class RedditComment
    {
        public static string Clean(this string input)
        {
            var i = input;
            var pattern = new Regex(@"\(\d\Wchild[ren]{0,3}\)(.)");

            i = i.Substring(pattern.Match(i).Groups[1].Index);
            i = Regex.Replace(i, @"\n", " ");
            i = Regex.Replace(i, @"</form>permalink", "");
            i = i.TrimEnd(' ');
            return i;
        }
    }
}
