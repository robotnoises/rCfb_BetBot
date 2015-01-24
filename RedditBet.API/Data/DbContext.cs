using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RedditBet.API.Data
{
    using RedditBet.API.Models;

    [DbConfigurationType("RedditBet.API.App_Start.DbConfig, RedditBet.API")]
    public class RedditBetDataContext : DbContext
    {
        // Context
        public RedditBetDataContext() : base("RedditBet") { }

        // Entities
        public DbSet<BotTask> Tasks { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<TempPageData> TempPages { get; set; }
        public DbSet<Fulfillment> Fulfillments { get; set; }
        public DbSet<BlacklistEntry> Blacklist { get; set; }
    }
}