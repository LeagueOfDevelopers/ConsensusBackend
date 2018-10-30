using System;
using System.Collections.Generic;
using System.Linq;
using ConsensusLibrary.DebateContext.Exceptions;
using ConsensusLibrary.Tools;
using EnsureThat;

namespace ConsensusLibrary.DebateContext
{
    public class Debate
    {
        public Debate(
            DateTimeOffset startDateTime,
            string title,
            Identifier inviterIdentifier,
            Identifier invitedIdentifier,
            DebateCategory debateCategory)
        {
            Ensure.Any.IsNotDefault(startDateTime);
            Ensure.String.IsNotNullOrWhiteSpace(title);
            Ensure.Any.IsNotNull(inviterIdentifier);
            Ensure.Any.IsNotNull(invitedIdentifier);

            Identifier = new Identifier();
            StartDateTime = startDateTime;
            Title = title;
            DebateCategory = debateCategory;
            _messages = new List<Message>();

            _members = new List<DebateMember>
            {
                new DebateMember(MemberRole.Opponent, inviterIdentifier, string.Empty),
                new DebateMember(MemberRole.Opponent, invitedIdentifier, string.Empty)
            };
        }

        public Identifier Identifier { get; }
        public DateTimeOffset StartDateTime { get; }
        public string Title { get; }
        public DebateCategory DebateCategory { get; }
        public IEnumerable<DebateMember> Members => _members;
        public IEnumerable<VoteInfo> Votes => _votes;
        public IEnumerable<Message> Messages => _messages;
        private List<DebateMember> _members { get; }
        private List<VoteInfo> _votes { get; }
        private List<Message> _messages { get; }

        internal void MakeVote(Identifier fromUser, Identifier toUser)
        {
            Ensure.Any.IsNotNull(fromUser);
            Ensure.Any.IsNotNull(toUser);
            Ensure.Bool.IsTrue(DateTimeOffset.UtcNow > StartDateTime, nameof(StartDateTime),
                opt => opt.WithException(new InvalidOperationException()));

            var debaters = _members.Where(m => m.MemberRole == MemberRole.Opponent).ToList();

            Ensure.Bool.IsTrue(toUser == debaters[0].UserIdentifier || toUser == debaters[1].UserIdentifier, nameof(toUser),
                opt => opt.WithException(new InvalidOperationException()));

            Ensure.Bool.IsFalse(_votes.Any(v => v.FromUser == fromUser), nameof(fromUser),
                opt => opt.WithException(new AlreadyVotedException()));

            _votes.Add(new VoteInfo(fromUser, toUser));
        }

        internal void AddMessage(Message message)
        {
            Ensure.Any.IsNotNull(message);
            _messages.Add(message);
        }
    }
}