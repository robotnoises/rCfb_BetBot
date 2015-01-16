using System;
using System.Linq;
using System.Collections.Generic;
using RedditBet.Bot.Enums;

namespace RedditBet.Bot.Models
{
    public class BotTask
    {
        public int TaskId { get; set; }
        public TaskType TaskType { get; set; }
        public string HashId { get; set; }
        public TaskData Data { get; set; }
        public string Message { get; set; }
        public bool Completed { get; set; }
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
        public string Key { get; set; }
        public string Value { get; set; }

        public TaskDataItem(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
