using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RedditBet.API.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RedditBet.API.Data
{
    [DbConfigurationType("RedditBet.API.App_Start.DbConfig, RedditBet.API")]
    public class RedditBetDataContext : IdentityDbContext<ApplicationUser>
    {
        // Construct Context using "RedditBet" as the conn string name
        public RedditBetDataContext() : base("RedditBet", throwIfV1Schema: false) { }

        // Entities
        public DbSet<BotTask> Tasks { get; set; }

    }
    public static class DatabaseContext
    {
        public static RedditBetDataContext Create()
        {
            return new RedditBetDataContext();
        }
    }
}