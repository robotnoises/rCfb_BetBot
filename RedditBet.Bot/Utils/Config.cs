using System;
using System.Collections.Generic;
using System.Configuration;

namespace RedditBet.Bot.Utils
{
    using Newtonsoft.Json;
    using RedditBet.Bot.Models;
    using Local = RedditBet.Bot.Properties;

    internal static class Config
    {
        #region Urls

        internal static string RedditBaseUrl { get { return ConfigurationManager.AppSettings["baseUrl"]; } }
        internal static string SubReddit { get { return ConfigurationManager.AppSettings["subReddit"]; } }
        internal static int UrlLimit { get { return Convert.ToInt32(ConfigurationManager.AppSettings["urlLimit"]); } }
        
        internal static string Reddit_Username { get { return ConfigurationManager.AppSettings["redditUsername"]; } }
        internal static string Reddit_Password { get { return ConfigurationManager.AppSettings["redditPassword"]; } }

        internal static string RedditApi_GetUser { get { return RedditBaseUrl + ConfigurationManager.AppSettings["redditApi_getUser"]; } }
        internal static string RedditApi_Login { get { return RedditBaseUrl + ConfigurationManager.AppSettings["redditApi_login"]; } }

        // RedditBet.API
        internal static string ApiUrl { get { return ConfigurationManager.AppSettings["apiBaseUrl"]; } }
        
        // Tasks
        internal static string Api_Tasks { get { return ConfigurationManager.AppSettings["api_Tasks"]; } }
        internal static string Api_Tasks_GetIncomplete { get { return ConfigurationManager.AppSettings["api_Tasks_Incomplete"]; } }
        internal static string Api_Tasks_MarkTaskComplete { get { return ConfigurationManager.AppSettings["api_Tasks_MarkComplete"]; } }
        internal static string Api_Tasks_Unique { get { return ConfigurationManager.AppSettings["api_Tasks_Unique"]; } }
        
        // Log
        internal static string Api_Log { get { return ConfigurationManager.AppSettings["api_Log"]; } }

        #endregion
               
        #region Local Resources

        /// <summary>
        /// Gets a Collection of key phrases to be used in matching reddit comments
        /// </summary>
        /// <returns>A Dictionary of key words and their values</returns>
        internal static ICollection<string> GetPhrasesToMatch()
        {
            var json = JsonConvert.DeserializeObject<Phrases>(RedditBet.Bot.Properties.Resources.phrases);

            return json.phrases;
        }

        /// <summary>
        /// Gets a collection of key phrases to be used in matching Reddit Users replies to a Bot comment
        /// </summary>
        /// <returns>A collection of </returns>
        internal static ICollection<string> GetConfirmationPhrases()
        {
            var json = JsonConvert.DeserializeObject<Phrases>(RedditBet.Bot.Properties.Resources.confirm);

            return json.phrases;
        }

        /// <summary>
        /// Gets a collection of key phrases to be used in matching Reddit Users replies to a Bot comment
        /// </summary>
        /// <returns>A collection of </returns>
        internal static ICollection<string> GetDeclinePhrases()
        {
            var json = JsonConvert.DeserializeObject<Phrases>(RedditBet.Bot.Properties.Resources.decline);

            return json.phrases;
        }

        /// <summary>
        /// Gets a collection of key phrases to be used in matching Reddit Users replies to a Bot comment
        /// </summary>
        /// <returns>A collection of </returns>
        internal static ICollection<string> GetUnsubscribePhrases()
        {
            var json = JsonConvert.DeserializeObject<Phrases>(RedditBet.Bot.Properties.Resources.unsubscribe);

            return json.phrases;
        }

        // Markdown files

        internal static string MarkDown_Test = Local.Resources.test;
        internal static string MarkDown_Greetings = Local.Resources.greetings;

        #endregion

        #region Special Keys

        internal static string TargetUrl_Key { get { return ConfigurationManager.AppSettings["targetUrl"]; } }
        internal static string Username_Key { get { return ConfigurationManager.AppSettings["userName"]; } }
        internal static string Message_Key { get { return ConfigurationManager.AppSettings["message"]; } }
        internal static string HashId_Key { get { return ConfigurationManager.AppSettings["hashId"]; } }
        internal static string Upvotes_Key { get { return ConfigurationManager.AppSettings["upvotes"]; } }
        internal static string ThreadUsernames_Key { get { return ConfigurationManager.AppSettings["threadUsernames"]; } }
                
        #endregion

        #region Misc

        // User agent 
        internal const string Bot_UserAgent = "cfb_betbot (still testing) 0.1 @robotnoises";

        #endregion
    }
}
