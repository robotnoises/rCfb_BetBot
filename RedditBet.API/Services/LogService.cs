using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedditBet.API.Repositories;
using RedditBet.API.Data;
using RedditBet.API.Models;
using System.Threading.Tasks;

namespace RedditBet.API.Services
{
    public class LogService
    {
        private LogRepository _repository;

        public LogService()
        {
            _repository = new LogRepository(DatabaseContext.Create());
        }

        public IEnumerable<Log> GetAll()
        {
            return _repository.GetAll();
        }

        public Log Get(int id)
        {
            return _repository.Get(id);
        }

        public void Create(Log t)
        {
            t.TimeStamp = DateTime.UtcNow;

            _repository.Add(t);
        }

        public void Update(Log t)
        {
            _repository.Update(t);
        }

        public void Remove(int id)
        {
            var t = _repository.Get(id);

            if (t != null)
            {
                _repository.Remove(t);
            }
        }
    }
}