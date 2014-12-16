using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Net;
using System.Web;
using System.Reflection;
using System.Configuration;
using RedditBet.Bot.Utils;

namespace RedditBet.Bot
{
    class Program
    {
        /* Bot stuff: */

        // Todo: Bot needs to be able to pick up "Tasks" beyond just searching comments
        // Todo: Bot needs to be able to send DMs
        // Todo: Bot needs to be able to update replies

        /* Other stuff: */

        // Add Tables for Comments (user), Tasks (for bot to carry out), Replies (Bot-only replies)

        static void Main(string[] args)
        {
            Log.Info("Bot is starting.");
            
            var comments = new Comments();

            foreach (var url in Config.GetUrls())
            {
                Log.Info("Fetching URLs.");

                var crawler = new Crawler(@"http://rc.reddit.com/r/CFB/comments/1rkt6s/week_14_user_friendly_bet_thread/");
                // var crawler = new Crawler(url);
                var matches = crawler.GetMatchedComments("class", "entry", GetWords(), 0.7);

                Log.Info(string.Format("Found {0} matches in {1}", matches.Count, url));
                
                comments.AddRange(matches);
            }
            
            Log.Info("Bot has finished");
            Console.ReadKey();
        }

        static Dictionary<string, double> GetWords()
        {
            var json = JsonConvert.DeserializeObject<Words>(RedditBet.Bot.Properties.Resources.words);
            return json.words;
        }
    }

    public class Words
    {
        public string title { get; set; }
        public string description { get; set; }
        public Dictionary<string, double> words { get; set; }
    }
}

