using System;
using System.Collections.Generic;

namespace RedditBet.API.Services
{
    using RedditBet.API.Models;
    using RedditBet.API.Repositories;
    using RedditBet.API.Data;

    public class BlacklistService
    {
        private Repository<BlacklistEntry> _repository;

        public BlacklistService()
        {
            _repository = new Repository<BlacklistEntry>(new UnitOfWork(DatabaseContext.Create()));
        }

        public IEnumerable<BlacklistEntry> GetAll()
        {
            return _repository.GetAll();
        }

        public BlacklistEntry Get(int id)
        {
            return _repository.Get(id);
        }

        public void Create(BlacklistEntry entity)
        {
            _repository.Add(entity);
        }

        public void Update(BlacklistEntry entity)
        {
            _repository.Update(entity);
        }

        public void Remove(int id)
        {
            var item = _repository.Get(id);

            if (item != null)
            {
                _repository.Remove(item);
            }
        }
    }
}