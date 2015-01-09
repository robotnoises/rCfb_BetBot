using System;
using System.Collections.Generic;
using System.Configuration;

namespace RedditBet.Bot.Utils
{
    public static class Config
    {
        public static string BaseUrl { get { return ConfigurationManager.AppSettings["baseUrl"]; } }
        public static string SubReddit { get { return ConfigurationManager.AppSettings["subReddit"]; } }
        public static int UrlLimit { get { return Convert.ToInt32(ConfigurationManager.AppSettings["urlLimit"]); } }
        
        public static string Reddit_Username { get { return ConfigurationManager.AppSettings["redditUsername"]; } }
        public static string Reddit_Password { get { return ConfigurationManager.AppSettings["redditPassword"]; } }

        public static string RedditApi_GetUser { get { return BaseUrl + ConfigurationManager.AppSettings["redditApi_getUser"]; } }
        public static string RedditApi_Login { get { return BaseUrl + ConfigurationManager.AppSettings["redditApi_login"]; } }

        // RedditBet.API
        public static string ApiUrl { get { return ConfigurationManager.AppSettings["apiBaseUrl"]; } }
        
        // Tasks
        public static string Api_Tasks { get { return ConfigurationManager.AppSettings["api_Tasks"]; } }
        public static string Api_Tasks_GetIncomplete { get { return ConfigurationManager.AppSettings["api_Tasks_Incomplete"]; } }
        public static string Api_Tasks_Update { get { return ConfigurationManager.AppSettings["api_Tasks_Incomplete"]; } }

        // Log
                
        public static string Api_Log { get { return ConfigurationManager.AppSettings["api_Log"]; } }
        

        public const string Bot_UserAgent = "cfb_betbot 0.1 @robotnoises";
    }
}
