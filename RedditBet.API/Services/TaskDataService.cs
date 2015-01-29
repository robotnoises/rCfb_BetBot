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
        private IRepository<TaskDataItem> _repo;

        public TaskDataService()
        {
            _repo = new Repository<TaskDataItem>(DatabaseContext.Create());
        }

        public TaskDataService(DbContext context)
        {
            _repo = new Repository<TaskDataItem>(context);
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
                var dataItem = _repo.GetWhere(x => x.Key == item.Key && x.Value == item.Value);

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
        }
    }
}
