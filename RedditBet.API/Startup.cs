using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using RedditBet.API.App_Start;
using System.Data.Entity;
using RedditBet.API.Data;
using System.Configuration;

[assembly: OwinStartup(typeof(RedditBet.API.Startup))]
namespace RedditBet.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var isDebug = ConfigurationManager.AppSettings["mode"] == "debug";
            if (isDebug) Database.SetInitializer<RedditBetDataContext>(new DropCreateDatabaseIfModelChanges<RedditBetDataContext>());

        }
    }
}
