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
        private string _name;
        private string _linkName;
        private const string _taskName = "Reply";

        public Reply(BotTask task)
        {
            var permaLink = task.Data.GetValue(Config.TargetUrl_Key);
            var parser = new PermaLinkParser(permaLink);

            _taskId = task.TaskId;
            _permaLink = permaLink;
            _message = task.Message;
            _name = parser.GetNameId();
            _linkName = parser.GetLinkId();
        }

        public void Execute()
        {
            base.StartTimer();

            var user = _redditContext.GetUser(Config.Reddit_Username);
            var parentComment = _redditContext.GetComment(Config.SubReddit, _name, _linkName);

            try
            {
                var botComment = parentComment.Reply(Message.Test());

                // Mark Task as Complete
                Api.MarkTaskComplete(_taskId);
                
                // Start monitoring this reply
                Api.AddMonitorTask(_name, _permaLink);

                // Todo: Log all this

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
