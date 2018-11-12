using System;
using ConsensusLibrary.Tools;

namespace ConsensusLibrary.DebateContext
{
    public class Message
    {
        public Message(Identifier userId, Identifier debateId, string text)
        {
            Id = new Identifier();
            UserId = userId;
            DebateId = debateId;
            Text = text;
            SentOn = DateTimeOffset.UtcNow;
        }

        internal Message(Identifier userId, Identifier debateId, string text, DateTimeOffset sentOn) : this(userId,
            debateId, text)
        {
            SentOn = sentOn;
        }

        public Identifier Id { get; }
        public Identifier UserId { get; }
        public Identifier DebateId { get; }
        public string Text { get; }
        public DateTimeOffset SentOn { get; }
    }
}