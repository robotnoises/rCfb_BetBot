using System;
using System.Collections.Generic;

namespace RedditBet.Bot.Utils
{
    using AutoMapper;
    using RedditBet.Bot.Models;

    public class TaskAble<T> where T : TaskAble<T>
    {
        private TaskAble<T> _derived 
        { 
            get 
            {
                return (T)this;
            } 
        }

        public BotTask ToBotTask(TaskData data, TaskType taskType = TaskType.Reply)
        {
            Mapper.CreateMap<T, BotTask>();
            
            var mapped = Mapper.Map<BotTask>(_derived);
            mapped.TaskData.AddRange(data);

            return mapped;
        }
    }
}
