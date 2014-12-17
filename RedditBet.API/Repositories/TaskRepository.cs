using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedditBet.API.Models;
using RedditBet.API.Data;
using System.Data.Entity;

namespace RedditBet.API.Repositories
{
    public class TaskRepository : IRepository<Task>
    {
        private readonly RedditBetDataContext _context;

        public TaskRepository(RedditBetDataContext context) {
            _context = context;
        }

        public IEnumerable<Task> GetAll()
        {
            return _context.Tasks.ToList();
        }

        public Task Get(int id)
        {
            return _context.Tasks.Find(id);
        }

        public void Add(Task entity)
        {
            _context.Tasks.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Task entity)
        {
            _context.Tasks.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(Task entity)
        {
            _context.Tasks.Remove(entity);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}