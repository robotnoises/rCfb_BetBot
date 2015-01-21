using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RedditBet.API.Data
{
    using RedditBet.API.Models;

    [DbConfigurationType("RedditBet.API.App_Start.DbConfig, RedditBet.API")]
    public class RedditBetDataContext : DbContext
    {
        // Construct Context using "RedditBet" as the conn string name
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
            return new RedditBetDataContext();
        }
    }
}