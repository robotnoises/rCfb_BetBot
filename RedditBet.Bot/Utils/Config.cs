using System;
using System.Collections.Generic;
using System.Configuration;

namespace RedditBet.Bot.Utils
{
    public static class Config
    {
        public static string BaseUrl { get { return ConfigurationManager.AppSettings["baseUrl"]; } }
        public static string SubReddit { get { return ConfigurationManager.AppSettings["subReddit"]; } }
        public static string ApiUrl { get { return ConfigurationManager.AppSettings["apiBaseUrl"]; } }
        public static string Api_Tasks { get { return ConfigurationManager.AppSettings["api_Tasks"]; } }
        public static string Api_Tasks_Incomplete { get { return ConfigurationManager.AppSettings["api_Tasks_Incomplete"]; } }
    }
}
