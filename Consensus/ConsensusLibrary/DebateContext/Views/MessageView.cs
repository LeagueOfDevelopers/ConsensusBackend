using System;
using ConsensusLibrary.Tools;

namespace ConsensusLibrary.DebateContext.Views
{
    public class MessageView
    {
        public Identifier MessageId { get; }
        public Identifier UserId { get; }
        public DateTimeOffset SentOn { get; }
        public string Text { get; }
        public string UserName { get; }

        public MessageView(
            Identifier messageId,
            Identifier userId,
            DateTimeOffset sentOn,
            string text, 
            string userName)
        {
            MessageId = messageId;
            UserId = userId;
            SentOn = sentOn;
            Text = text;
            UserName = userName;
        }
    }
}
