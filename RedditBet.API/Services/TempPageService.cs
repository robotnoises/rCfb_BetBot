using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;

namespace RedditBet.API.Services
{
    using RedditBet.API.Data;
    using RedditBet.API.Models;
    using RedditBet.API.Repositories;
    
    internal class TempPageService
    {
        private IUnitOfWork<TempPage> _uow;

        public TempPageService()
        {
            _uow = new UnitOfWork<TempPage>(DatabaseContext.Create());
        }

        public TempPageStatus ValidateToken(string token)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}