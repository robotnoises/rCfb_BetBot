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
        private IUnitOfWork<BotTask> _uow;

        public TaskService()
        {
            _uow = new UnitOfWork<BotTask>(DatabaseContext.Create());
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

        public void Create(BotTask t)
        {
            if (TaskAlreadyExists(t)) return;
            
            t.TimeAssigned = DateTime.UtcNow;
            t.TimeCompleted = null;
            t.Completed = false;

            _uow.Add(t);
        }

        public void Update(BotTask t)
        {
            _uow.Update(t);
        }

        public void MarkComplete(int id)
        {
            var t = _uow.Get(id);

            if (t == null) return;

            t.Completed = true;
            t.TimeCompleted = DateTime.UtcNow;

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

        private bool TaskAlreadyExists(BotTask t)
        {
            var task = _uow.GetWhere(x => x.HashId == t.HashId).OrderByDescending(x => x.TimeAssigned).FirstOrDefault();
                        
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