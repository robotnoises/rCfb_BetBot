using System;
using System.Data.Entity;
using System.Collections.Generic;

namespace RedditBet.API.Services
{
    using RedditBet.API.Repositories;
    using RedditBet.API.Models;

    public class FulfillmentService
    {
        private readonly IUnitOfWork<Fulfillment> _uow;

        public FulfillmentService()
        {
            _uow = new UnitOfWork<Fulfillment>(DatabaseContext.Create());
        }

        public FulfillmentService(DbContext context)
        {
            _uow = new UnitOfWork<Fulfillment>(context);
        }

        public void Add(Fulfillment f)
        {
            _uow.Add(f);
        }

        public void Update(Fulfillment f)
        {
            _uow.Update(f);
        }

        public void Remove(Fulfillment f)
        {
            _uow.Remove(f);
        }
    }
}