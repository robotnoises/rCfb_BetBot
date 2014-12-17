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
using RedditBet.Bot.Data;
using RedditBet.Bot.Enums;

namespace RedditBet.Bot
{
    class Program
    {
        /* Bot stuff: */

        // Todo: Bot (Crawling) need to be able to get the "thing" id for replies 
        // Todo: Bot needs to be able to pick up "Tasks" beyond just searching comments
        // Todo: Bot needs to be able to send DMs
        // Todo: Bot needs to be able to update replies
        // Todo: Bot needs to accept arguments to either Crawl or pick up tasks from the API
        // Todo: Bot needs a TaskFactory to carry-out various tasks via a common task interface
        // Todo: Bot needs to be able to check if logged into reddit, and log in
        // Todo: Bot needs to time itself

        /* Other stuff: */

        // Add Tables for Comments (user), Tasks (for bot to carry out), Replies (Bot-only replies)
        // Add Logging
        // Add new Web project to serve (initially) as the place where users confirm bets

        static void Main(string[] args)
        {
            Log.Info("Bot is starting.");
            
            var comments = new Comments();

            foreach (var url in Config.GetUrls())
            {
                Log.Info("Fetching URLs.");

                var crawler = new Crawler(url);
                var matches = crawler.GetMatchedComments("class", "entry", Config.GetTargetWords(), 0.7);

                Log.Info(string.Format("Found {0} matches in {1}", matches.Count, url));
                
                comments.AddRange(matches);
            }

            Log.Info("Bot has finished");
        }
    }
}

