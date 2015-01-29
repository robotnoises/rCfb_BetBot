using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace RedditBet.API.Services
{
    using RedditBet.API.Models;
    using RedditBet.API.Repositories;

    public class LogService
    {
        private IRepository<Log> _repo;

        public LogService()
        {
            _repo = new Repository<Log>(DatabaseContext.Create());
        }

        public LogService(DbContext context)
        {
            _repo = new Repository<Log>(context);
        }

        public IEnumerable<Log> GetAll()
        {
            return _repo.GetAll();
        }

        public Log Get(int id)
        {
            return _repo.Get(id);
        }

        public void Create(Log t)
        {
            t.TimeStamp = (t.TimeStamp != null) ? t.TimeStamp : DateTime.UtcNow;

            _repo.Add(t);
        }

        public void Update(Log t)
        {
            _repo.Update(t);
        }

        public void Remove(int id)
        {
            var t = _repo.Get(id);

            if (t != null)
            {
                _repo.Remove(t);
            }
        }
    }
}