using System;

namespace RedditBet.Bot.Tasks
{
    using RedditBet.Bot.Models;
    using RedditBet.Bot.Utils;

    /// <summary>
    /// Updates an existing reply
    /// </summary>
    public class UpdateReply : RedditTask, IBotTask
    {
        private string _targetUrl;
        private string _message;
        private string _name;
        private string _linkName;
        private const string _taskName = "UpdateReply";

        public UpdateReply(BotTask task)
        {
            var permaLink = task.TaskData.GetValue(Config.TargetUrl_Key);
            var parser = new PermaLinkParser(permaLink);

            _targetUrl = permaLink;
            _message = task.TaskData.GetValue(Config.Message_Key);
            _name = parser.GetNameId();
            _linkName = parser.GetLinkId();
        }

        public void Execute()
        {
            base.StartTimer();

            var comment = _redditContext.GetComment(Config.SubReddit, _name, _linkName);

            // comment.Reply(_message);

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
