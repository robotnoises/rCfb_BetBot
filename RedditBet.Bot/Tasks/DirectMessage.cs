using System;

namespace RedditBet.Bot.Tasks
{
    using RedditBet.Bot.Models;
    using RedditBet.Bot.Utils;
    using RedditBet.Bot.DataResources;

    /// <summary>
    /// Send a direct message to a reddit user
    /// </summary>
    public class DirectMessage : RedditTask, IBotTask
    {
        private BotTask _task;
        private const string _taskName = "DirectMessage";

        public DirectMessage(BotTask task)
        {
            _task = task;
        }

        public void Execute()
        {
            base.StartTimer();

            try
            {
                var subject = "Test";
                var body = _task.TaskData.GetValue(Config.Message_Key);
                var to = _task.TaskData.GetValue(Config.Username_Key);

                // _redditContext.ComposePrivateMessage(subject, body, to);
                
                Api.MarkTaskComplete(_task.TaskId);
            }
            catch (Exception ex)
            {
                // Todo
            }

            base.StopTimer();
        }

        public string GetTaskName()
        {
            return _taskName;
        }

        public TimeSpan GetElapsedTime()
        {
            return base.GetElapsed();
        }
    }
}
