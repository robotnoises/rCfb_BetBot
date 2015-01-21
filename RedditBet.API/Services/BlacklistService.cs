using System;
using System.Collections.Generic;

namespace RedditBet.API.Services
{
    using RedditBet.API.Models;
    using RedditBet.API.Repositories;
    using RedditBet.API.Data;

    public class BlacklistService
    {
        private IUnitOfWork<BlacklistEntry> _uow;

        public BlacklistService()
        {
            _uow = new UnitOfWork<BlacklistEntry>(DatabaseContext.Create());
        }

        public BlacklistEntry Get(int id)
        {
            return _uow.Get(id);
        }

        public IEnumerable<BlacklistEntry> GetAll()
        {
            return _uow.GetAll();
        }

        public void Create(BlacklistEntry entity)
        {
            _uow.Add(entity);
        }

        public void Update(BlacklistEntry entity)
        {
            _uow.Update(entity);
        }

        public void Remove(int id)
        {
            var item = _uow.Get(id);

            if (item != null)
            {
                _uow.Remove(item);
            }
        }
    }
}