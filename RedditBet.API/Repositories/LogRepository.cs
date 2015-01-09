using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedditBet.API.Models;
using RedditBet.API.Data;
using System.Data.Entity;

namespace RedditBet.API.Repositories
{
    public class LogRepository : IRepository<Log>
    {
        private readonly RedditBetDataContext _context;

        public LogRepository(RedditBetDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Log> GetAll()
        {
            return _context.Logs.ToList();
        }

        public Log Get(int id)
        {
            return _context.Logs.Find(id);
        }

        public void Add(Log entity)
        {
            _context.Logs.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Log entity)
        {
            _context.Logs.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(Log entity)
        {
            _context.Logs.Remove(entity);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}