using System;
using Owin;
using Microsoft.Owin;
using System.Data.Entity;
using System.Configuration;
using System.Web.Http;

[assembly: OwinStartup(typeof(RedditBet.API.Startup))]
namespace RedditBet.API
{
    using RedditBet.API.Data;
    
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var isDebug = ConfigurationManager.AppSettings["mode"] == "debug";
            if (isDebug) Database.SetInitializer<RedditBetDataContext>(new DropCreateDatabaseIfModelChanges<RedditBetDataContext>());
            
            // So that Newtonsoft can serialize 1 to 1 relationships, which EF seems to be not good at.
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
