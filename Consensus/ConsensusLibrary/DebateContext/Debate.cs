using System;
using System.Collections.Generic;
using ConsensusLibrary.Tools;
using EnsureThat;

namespace ConsensusLibrary.DebateContext
{
    public class Debate
    {
        public Identifier Identifier { get; private set; }
        public DateTimeOffset StartDateTime { get; private set; }
        public DateTimeOffset EndDateTime { get; private set; }
        public string Title { get; private set; }
        public DebateCategory DebateCategory { get; private set; }
        public IEnumerable<DebateMember> Members { get { return _members; } }
        private List<DebateMember> _members { get; set; }

        public Debate(
            DateTimeOffset startDateTime,
            DateTimeOffset endDateTime,
            string title,
            Identifier inviterIdentifier, 
            Identifier invitedIdentifier,
            DebateCategory debateCategory)
        {
            Ensure.Any.IsNotDefault(startDateTime);
            Ensure.Any.IsNotDefault(endDateTime);
            Ensure.String.IsNotNullOrWhiteSpace(title);
            Ensure.Any.IsNotNull(inviterIdentifier);
            Ensure.Any.IsNotNull(invitedIdentifier);

            Identifier = new Identifier();
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            Title = title;
            DebateCategory = debateCategory;

            _members = new List<DebateMember>()
            {
                new DebateMember(MemberRole.Opponent, inviterIdentifier, string.Empty),
                new DebateMember(MemberRole.Opponent, invitedIdentifier, string.Empty)
            };
        }
    }
}
