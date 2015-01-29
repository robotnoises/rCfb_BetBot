using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace RedditBet.API.Services
{
    using RedditBet.API.Models;
    using RedditBet.API.Repositories;

    public class TaskService
    {
        private IRepository<BotTask> _repo;

        public TaskService()
        {
            _repo = new Repository<BotTask>(DatabaseContext.Create());
        }

        public TaskService(DbContext context)
        {
            _repo = new Repository<BotTask>(context);
        }

        public IEnumerable<BotTask> GetAll() {
            return _repo.GetAll();
        }

        public IEnumerable<BotTask> GetIncomplete()
        {
            var foo = _repo.GetAll().Where(x => !x.Completed);
            return foo;
        }

        public BotTask Get(int id)
        {
            return _repo.Get(id);
        }

        public void Add(BotTask t)
        {
            t.TimeAssigned = DateTime.UtcNow;
            t.TimeCompleted = null;
            t.Completed = false;

            _repo.Add(t);
        }

        public void Update(BotTask t)
        {
            t.TimeLastRun = DateTime.UtcNow;

            if (t.Completed && t.TimeCompleted == null) 
            {
                // Todo: throw Exception explaining they used the wrong method to mark this Task complete
            }

            _repo.Update(t);
        }

        public void MarkComplete(int id)
        {
            var t = _repo.Get(id);

            if (t == null) return;

            t.Completed = true;
            t.TimeCompleted = DateTime.UtcNow;
            t.TimeLastRun = t.TimeCompleted;

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