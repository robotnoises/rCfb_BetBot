using System;
using System.Linq;
using System.Collections.Generic;

namespace RedditBet.Bot.Models
{
    public class BotTask
    {
        public int TaskId { get; set; }
        public TaskType TaskType { get; set; }
        public DateTime? TimeLastRun { get; set; }
        public bool Completed { get; set; }
        public TaskData TaskData { get; set; }

        public BotTask(TaskType type = TaskType.Reply)
        {
            TaskType = type;
        }
    }

    public class TaskData : List<TaskDataItem>
    {
        private HashSet<string> _keys;

        public TaskData()
        {
            _keys = new HashSet<string>();
        }

        public TaskDataItem Get(string key)
        {
            return this.Where(x => x.Key == key).FirstOrDefault();
        }
        
        public string GetValue(string key)
        {
            var value = "";

            try
            {
                value = this.Where(x => x.Key == key).Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception ex)
            { 
                // Todo: throw new TaskDataKeyNotFound()
            }

            return value;
        }

        public void Add(string key, string value)
        {
            if (!_keys.Contains(key)) DoAdd(key, value);
        }

        public void AddRange(Dictionary<string, string> items)
        {
            foreach (var item in items)
            {
                if (!_keys.Contains(item.Key)) DoAdd(item.Key, item.Value);
            }
        }

        private void DoAdd(string key, string value)
        {
            _keys.Add(key);
            this.Add(new TaskDataItem(key, value));
        }
    }

    public class TaskDataItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
        
        public TaskDataItem(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    // Todo: expand on this
    public class UniqueTaskResponse
    {
        public bool IsUnique { get; set; }
    }
}
