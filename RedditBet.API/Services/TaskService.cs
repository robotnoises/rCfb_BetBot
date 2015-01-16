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

        public IEnumerable<BotTask> GetAll() {
            return _repository.GetAll();
        }

        public IEnumerable<BotTask> GetIncomplete()
        {
            var foo = _repository.GetAll().Where(x => !x.Completed);
            return foo;
        }

        public BotTask Get(int id)
        {
            return _repository.Get(id);
        }

        public void Create(BotTask t)
        {
            if (TaskAlreadyExists(t)) return;
            
            t.TimeAssigned = DateTime.UtcNow;
            t.TimeCompleted = null;
            t.Completed = false;

            _repository.Add(t);
        }

        public void Update(BotTask t)
        {
            _repository.Update(t);
        }

        public void MarkComplete(int id)
        {
            var t = _repository.Get(id);

            if (t == null) return;

            t.Completed = true;
            t.TimeCompleted = DateTime.UtcNow;

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

        private bool TaskAlreadyExists(BotTask t)
        {
            var task = _repository.Get(t.HashId).OrderByDescending(x => x.TimeAssigned).FirstOrDefault();
            
            // If nothing is found return false
            if (task == null) return false;
                        
            if (task.TaskType == (int)TaskType.Reply)
            {
                // If the TaskType is "Reply", return true, as there is only allowed to be one "Reply" per comment
                return true;
            }
            else
            {
                // Else, the task is either a direct message or update, so just make sure the task is now complete
                return !t.Completed;
            }
        }
    }
}