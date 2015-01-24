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
        private IUnitOfWork<Log> _uow;

        public LogService()
        {
            _uow = new UnitOfWork<Log>(DatabaseContext.Create());
        }

        public LogService(DbContext context)
        {
            _uow = new UnitOfWork<Log>(context);
        }

        public IEnumerable<Log> GetAll()
        {
            return _uow.GetAll();
        }

        public Log Get(int id)
        {
            return _uow.Get(id);
        }

        public void Create(Log t)
        {
            t.TimeStamp = (t.TimeStamp != null) ? t.TimeStamp : DateTime.UtcNow;

            _uow.Add(t);
        }

        public void Update(Log t)
        {
            _uow.Update(t);
        }

        public void Remove(int id)
        {
            var t = _uow.Get(id);

            if (t != null)
            {
                _uow.Remove(t);
            }
        }
    }
}