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

        // Todo: need to look into this more. This is a stopgap for prevent orphaned recs when the parent Bet object is updated
        public void RemoveOldFulfillmentRecord(Fulfillment f)
        {
            _uow.Remove(f);
        }
    }
}