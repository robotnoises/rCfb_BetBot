using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedditBet.API.Models;
using RedditBet.API.Data;
using System.Data.Entity;

namespace RedditBet.API.Repositories
{
    public class BlacklistRepository : IRepository<BlacklistEntry>
    {
        private readonly RedditBetDataContext _context;

        public BlacklistRepository(RedditBetDataContext context)
        {
            _context = context;
        }

        public IEnumerable<BlacklistEntry> GetAll()
        {
            return _context.Blacklist.ToList();
        }

        public BlacklistEntry Get(int id)
        {
            return _context.Blacklist.Find(id);
        }

        public void Add(BlacklistEntry entity)
        {
            _context.Blacklist.Add(entity);
            _context.SaveChanges();
        }

        public void Update(BlacklistEntry entity)
        {
            _context.Blacklist.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(BlacklistEntry entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}