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
    public class RedditBetDataContext : DbContext // : IdentityDbContext<ApplicationUser>
    {
        // Construct Context using "RedditBet" as the conn string name
        // public RedditBetDataContext() : base("RedditBet", throwIfV1Schema: false) { }
        
        public RedditBetDataContext() : base("RedditBet") { }

        // Entities
        public DbSet<BotTask> Tasks { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<BlacklistEntry> Blacklist { get; set; }

    }

    public static class DatabaseContext
    {
        public static RedditBetDataContext Create()
        {
            // Todo: check here if context is disposed of? Created?
            return new RedditBetDataContext();
        }
    }
}