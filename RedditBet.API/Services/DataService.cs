using System;

namespace RedditBet.API.Services
{
    using RedditBet.API.Data;

    public static class DatabaseContext
    {
        public static RedditBetDataContext Create()
        {
            return new RedditBetDataContext();
        }
    }
}