using System;
using System.Data.Entity;
using System.Collections.Generic;

namespace RedditBet.API.Controllers
{
    using RedditBet.API.Models;
    using RedditBet.API.Repositories;
    using RedditBet.API.Services;

    internal class TaskDataService
    {
        private IUnitOfWork<TaskDataItem> _uow;

        public TaskDataService()
        {
            _uow = new UnitOfWork<TaskDataItem>(DatabaseContext.Create());
        }

        public TaskDataService(DbContext context)
        {
            _uow = new UnitOfWork<TaskDataItem>(context);
        }

        // This method is stupid.
        public bool TaskIsUnique(TaskDataItem item)
        {
            // Todo: Not all Tasks have a hashId... this really doesn't belong here, 
            // should probably be another API call since the client should techically 
            // only be aware of the keys for this dictionary

            if (item == null) return true;

            try
            {
                var dataItem = _uow.GetWhere(x => x.Key == item.Key && x.Value == item.Value);

                foreach (var d in dataItem)
                {
                    return false;
                }

                return true;
            }
            catch 
            {
                return true;
            }
                        
            //var task = _uow
            //    .GetWhere(x => x.TaskData.GetValue(hashIdKey) == hashId)
            //    .ToList()
            //    .OrderByDescending(x => x.TimeAssigned)
            //    .FirstOrDefault();

            // If nothing is found return false
            //if (task == null) return false;

            //if (task.TaskType == (int)TaskType.Reply)
            //{
            //    // If the TaskType is "Reply", return true, as there is only allowed to be one "Reply" per comment
            //    return true;
            //}
            //else
            //{
            //    // Else, the task is either a direct message or update, so just make sure the task is now complete
            //    return !data.Completed;
            //}
        }
    }
}
