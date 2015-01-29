using System;
using System.Data.Entity;
using System.Collections.Generic;

namespace RedditBet.API.Services
{
    using RedditBet.API.Models;
    using RedditBet.API.Repositories;
    
    public class BlacklistService
    {
        private IRepository<BlacklistEntry> _repo;

        public BlacklistService()
        {
            _repo = new Repository<BlacklistEntry>(DatabaseContext.Create());
        }

        public BlacklistService(DbContext context)
        {
            _repo = new Repository<BlacklistEntry>(context);
        }

        public BlacklistEntry Get(int id)
        {
            return _repo.Get(id);
        }

        public IEnumerable<BlacklistEntry> GetAll()
        {
            return _repo.GetAll();
        }

        public void Create(BlacklistEntry entity)
        {
            _repo.Add(entity);
        }

        public void Update(BlacklistEntry entity)
        {
            _repo.Update(entity);
        }

        public void Remove(int id)
        {
            var item = _repo.Get(id);

            if (item != null)
            {
                _repo.Remove(item);
            }
        }
    }
}