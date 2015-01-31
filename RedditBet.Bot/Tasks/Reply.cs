using System;

namespace RedditBet.Bot.Tasks
{
    using RedditSharp;
    using RedditBet.Bot.Utils;
    using RedditBet.Bot.Models;
    using RedditBet.Bot.DataResources;

    /// <summary>
    /// Replies to a comment
    /// </summary>
    public class Reply : RedditTask, IBotTask
    {
        private int _taskId;
        private string _permaLink;
        private string _message;
        private string _nameId;
        private string _linkId;
        private const string _taskName = "Reply";

        public Reply(BotTask task)
        {
            _permaLink = task.TaskData.GetValue(Config.TargetUrl_Key);
            _message = task.TaskData.GetValue(Config.Message_Key);
            _taskId = task.TaskId;
            _nameId = _permaLink.GetNameId();
            _linkId = _permaLink.GetLinkId();
        }

        public void Execute()
        {
            base.StartTimer();

            var parentComment = _redditContext.GetComment(Config.SubReddit, _nameId, _linkId);

            try
            {
                var botComment = new BetBotComment(parentComment.Reply(Message.Test()));

                // Mark Task as Complete
                Api.MarkTaskComplete(_taskId);

                var monitorTask = botComment.ToBotTask(TaskType.Monitor);
                
                monitorTask.TaskData.Add(new TaskDataItem(Config.TargetUrl_Key, _permaLink));
                
                // Start monitoring this reply
                Api.AddBotTask(monitorTask);

                // Todo: Log all of this

            }
            catch (RateLimitException ex)
            {
                Log.Error(ex);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
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
