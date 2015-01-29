using System;
using System.Data.Entity;
using System.Collections.Generic;

namespace RedditBet.API.Services
{
    using RedditBet.API.Repositories;
    using RedditBet.API.Models;

    public class FulfillmentService
    {
        private readonly IRepository<Fulfillment> _repo;

        public FulfillmentService()
        {
            _repo = new Repository<Fulfillment>(DatabaseContext.Create());
        }

        public FulfillmentService(DbContext context)
        {
            _repo = new Repository<Fulfillment>(context);
        }

        public void Add(Fulfillment f)
        {
            _repo.Add(f);
        }

        public void Update(Fulfillment f)
        {
            _repo.Update(f);
        }

        public void Remove(Fulfillment f)
        {
            _repo.Remove(f);
        }
    }
}