using System;

namespace RedditBet.Bot.Tasks
{
    using RedditBet.Bot.Models;
    using RedditBet.Bot.Utils;

    /// <summary>
    /// Send a direct message to a reddit user
    /// </summary>
    public class DirectMessage : RedditTask, IBotTask
    {
        private string _targetUrl;
        private string _message;
        private const string _taskName = "DirectMessage";

        public DirectMessage(BotTask task)
        {
            _targetUrl = task.TaskData.GetValue(Config.TargetUrl_Key);
            _message = task.TaskData.GetValue(Config.Message_Key);
        }

        public void Execute()
        {
            base.StartTimer();

            // todo

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
