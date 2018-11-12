using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using ConsensusLibrary.DebateContext.Exceptions;
using ConsensusLibrary.Tools;
using EnsureThat;

[assembly: InternalsVisibleTo("ConsensusLibrary.Tests")]
namespace ConsensusLibrary.DebateContext
{
    public class Debate
    {
        public Debate(
            DateTimeOffset startDateTime,
            string title,
            Identifier inviterIdentifier,
            Identifier invitedIdentifier,
            DebateCategory debateCategory,
            int roundCount,
            TimeSpan roundLength)
        {
            Ensure.Any.IsNotDefault(startDateTime);
            Ensure.String.IsNotNullOrWhiteSpace(title);
            Ensure.Any.IsNotNull(inviterIdentifier);
            Ensure.Any.IsNotNull(invitedIdentifier);
            Ensure.Bool.IsTrue(roundCount > 0 && roundCount % 2 == 0, nameof(roundCount),
                opt => opt.WithException(new ArgumentOutOfRangeException()));
            Ensure.Any.IsNotDefault(roundLength);

            Identifier = new Identifier();
            StartDateTime = startDateTime;
            RoundCount = roundCount;
            RoundLength = roundLength;
            Title = title;
            DebateCategory = debateCategory;
            _messages = new List<Message>();


            var inviterMember = new DebateMember(MemberRole.Opponent, inviterIdentifier, string.Empty);
            var invitedMember = new DebateMember(MemberRole.Opponent, invitedIdentifier, string.Empty);

            Rounds = FillRounds(roundCount, roundLength, inviterMember, invitedMember);

            _members = new List<DebateMember>
            {
                inviterMember,
                invitedMember
            };

            State = DebateState.Waiting;
        }

        public Identifier Identifier { get; }
        public DateTimeOffset StartDateTime { get; private set; }
        public DateTimeOffset EndDateTime => StartDateTime.Add(RoundLength * RoundCount);
        public string Title { get; }
        public DebateCategory DebateCategory { get; }
        public IEnumerable<DebateMember> Members => _members;
        public IEnumerable<VoteInfo> Votes => _votes;
        public IEnumerable<Message> Messages => _messages;
        public IEnumerable<Round> Rounds { get; private set; }
        private readonly List<DebateMember> _members; 
        private readonly List<VoteInfo> _votes;
        private readonly List<Message> _messages;
        public DebateState State { get; private set; }
        public int RoundCount { get; }
        public TimeSpan RoundLength { get; }

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

        internal void SetReadyStatus(Identifier userId)
        {
            Ensure.Any.IsNotNull(userId);

            var targetMember = Members.FirstOrDefault(m => m.UserIdentifier == userId);

            Ensure.Any.IsNotNull(targetMember, nameof(targetMember),
                opt => opt.WithException(new InvalidOperationException()));

            Ensure.Bool.IsTrue(State == DebateState.Waiting, nameof(State),
                opt => opt.WithException(new InvalidOperationException()));

            if (!IsPossibleToStart())
            {
                State = DebateState.Overdue;
                return;
            }

            targetMember.BecomeReady();
            
            TryToStartDebate();
        }

        internal void AddMessage(Message message)
        {
            Ensure.Any.IsNotNull(message);
            _messages.Add(message);
        }

        private IEnumerable<Round> FillRounds(int roundCount, TimeSpan roundLength, DebateMember inviter, DebateMember invited)
        {
            Ensure.Bool.IsTrue(State == DebateState.Waiting, nameof(State),
                opt => opt.WithException(new InvalidOperationException()));

            var result = new List<Round>();

            var roundStart = StartDateTime;

            for (var i = 0; i < roundCount; i++)
            {
                result.Add(new Round(roundStart, roundStart.Add(roundLength),
                    i % 2 == 0 ? inviter.UserIdentifier : invited.UserIdentifier));

                roundStart = roundStart.Add(roundLength);
            }

            return result;
        }

        private void TryToStartDebate()
        {
            if (Members.Any(m => !m.Ready))
                return;

            var membersList = Members.ToList();
            var inviter = membersList[0];
            var invited = membersList[1];
            StartDateTime = DateTimeOffset.UtcNow;
            Rounds = FillRounds(RoundCount, RoundLength, inviter, invited);
            State = DebateState.Approved;
        }

        private bool IsPossibleToStart()
        {
            var now = DateTimeOffset.UtcNow;
            return now - StartDateTime < new TimeSpan(0, 0, 2, 0); //TODO config values
        }
    }
}