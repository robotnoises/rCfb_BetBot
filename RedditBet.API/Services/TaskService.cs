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
        private IUnitOfWork<BotTask> _uow;

        public TaskService()
        {
            _uow = new UnitOfWork<BotTask>(DatabaseContext.Create());
        }

        public TaskService(DbContext context)
        {
            _uow = new UnitOfWork<BotTask>(context);
        }

        public IEnumerable<BotTask> GetAll() {
            return _uow.GetAll();
        }

        public IEnumerable<BotTask> GetIncomplete()
        {
            var foo = _uow.GetAll().Where(x => !x.Completed);
            return foo;
        }

        public BotTask Get(int id)
        {
            return _uow.Get(id);
        }

        public void Add(BotTask t)
        {
            t.TimeAssigned = DateTime.UtcNow;
            t.TimeCompleted = null;
            t.Completed = false;

            _uow.Add(t);
        }

        public void Update(BotTask t)
        {
            t.TimeLastRun = DateTime.UtcNow;

            if (t.Completed && t.TimeCompleted == null) 
            {
                // Todo: throw Exception explaining they used the wrong method to mark this Task complete
            }

            _uow.Update(t);
        }

        public void MarkComplete(int id)
        {
            var t = _uow.Get(id);

            if (t == null) return;

            t.Completed = true;
            t.TimeCompleted = DateTime.UtcNow;
            t.TimeLastRun = t.TimeCompleted;

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