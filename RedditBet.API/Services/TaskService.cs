using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedditBet.API.Repositories;
using RedditBet.API.Data;
using RedditBet.API.Models;

namespace RedditBet.API.Services
{
    public class TaskService
    {
        private TaskRepository _repository;

        public TaskService()
        {
            _repository = new TaskRepository(DatabaseContext.Create());
        }

        public IEnumerable<Task> GetAll() {
            return _repository.GetAll();
        }

        public IEnumerable<Task> GetIncomplete()
        {
            return _repository.GetAll().Where(x => !x.Completed);
        }

        public Task Get(int id)
        {
            return _repository.Get(id);
        }

        public void Create(Task t)
        {
            t.TimeAssigned = DateTime.UtcNow;
            t.TimeCompleted = null;
            t.Completed = false;

            _repository.Add(t);
        }

        public void Update(Task t)
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