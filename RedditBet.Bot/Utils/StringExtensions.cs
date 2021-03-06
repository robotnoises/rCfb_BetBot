﻿using System;
using System.Text;

namespace RedditBet.Bot.Utils
{
    using System.Security.Cryptography;
    using System.Text.RegularExpressions;

    public static class RedditComment
    {
        public static string ScrubRedditComment(this string input)
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

    public static class StringExtensions
    {
        public static string ToHashString(this string input)
        {
            var hash = "";
            var bytes = Encoding.UTF8.GetBytes(input);

            using (var hasher = new SHA1Managed())
            {
                try
                {
                    var h = hasher.ComputeHash(bytes);
                    var sb = new StringBuilder(h.Length * 2);

                    foreach (var b in h)
                    {
                        sb.Append(b.ToString("X2"));
                    }

                    hash = sb.ToString();
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }

            return hash;
        }

        public static string AppendRandomQueryString(this string input)
        {
            var random = new Random().Next(1, 9999);
            var url = input.Split(new char[] { '&', '?' });

            // Todo, confirm is URL, throw Exception if not

            // No existing query string
            if (url.Length == 1)
            {
                return input.TrimEnd(new char[] { '/' }) + string.Format("?={0}", random);
            }
            // has querystring(s)
            else
            {
                return input.TrimEnd(new char[] { '/' }) + string.Format("&={0}", random);
            }
        }

        public static Uri ToUri(this string url)
        {
            // Todo, confirm is URL, throw Exception if not
            return new Uri(url);
        }

        public static string GetNameId(this string permaLink)
        {
            // Todo, confirm is reddit permalink-style url

            if (string.IsNullOrEmpty(permaLink)) return string.Empty; // Todo throw exception

            var parts = permaLink.Split('/');
            var lastPart = parts.Length - 1;

            return string.Format("t1_{0}", parts[lastPart]);
        }

        public static string GetLinkId(this string permaLink)
        {
            // Todo, confirm is reddit permalink-style url

            if (string.IsNullOrEmpty(permaLink)) return string.Empty; // Todo throw exception

            var parts = permaLink.Split('/');
            var index = Array.IndexOf(parts, "comments") + 1;

            return string.Format("t3_{0}", parts[index]);
        }
    }
}
