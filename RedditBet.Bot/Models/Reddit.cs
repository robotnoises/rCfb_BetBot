using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditBet.Bot.Models
{
    #region Page

    public class RedditJSON
    {
        public string kind { get; set; }
        public RedditPage data { get; set; }
    }

    public class RedditPage
    {
        public string modhash { get; set; }
        public List<RedditPosts> children { get; set; }
        public string after { get; set; }
        public string before { get; set; }
    }

    public class RedditPosts
    {
        public string kind { get; set; }
        public RedditPostData data { get; set; }
    }

    public class RedditPostData
    {
        public string permalink { get; set; }
    }
    
    #endregion 

    #region Auth

    public class RedditUser
    {
        public string kind { get; set; }
        public AuthData data { get; set; }

        public bool IsLoggedIn()
        {
            return data != null;
        }
    }

    public class AuthData
    {
        public bool has_mail { get; set; }
        public int comment_karma { get; set; }
        public string name { get; set; }
    }

    public class Login
    {
        public string api_type;
        public bool rem;
        public string user;
        public string passwd;

        public Login(string username, string password)
        {
            api_type = "json";
            rem = true;
            user = username;
            passwd = password;
        }
    }

    public class LoginResponse
    {
        public string name { get; set; }
        public string cookie { get; set; }
    }

    #endregion
}
