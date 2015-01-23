using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RedditBet.API.Models
{
    public class BotTask
    {
        [Key]
        public int TaskId { get; set; }
        public string HashId { get; set; }
        public int TaskType { get; set; }
        public DateTime TimeAssigned { get; set; }
        public DateTime? TimeCompleted { get; set; }
        public bool Completed { get; set; }
        public string Message { get; set; }
        public virtual TaskData Data { get; set; }
    }

    public class BotTaskViewModel : Mappable<BotTaskViewModel, BotTask>
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public int TaskType { get; set; }
        public DateTime TimeAssigned { get; set; }
        public DateTime? TimeCompleted { get; set; }
        public bool Completed { get; set; }
        public string Message { get; set; }
        public virtual TaskData Data { get; set; }
    }

    public class TaskData : List<TaskDataItem>
    {
        private HashSet<string> _keys;

        public TaskData()
        {
            _keys = new HashSet<string>();
        }

        public string GetValue(string key)
        {
            return this.Where(x => x.Key == key).Select(x => x.Value).FirstOrDefault();
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
        [Key]
        public int TaskDataItemId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public TaskDataItem() { }

        public TaskDataItem(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}