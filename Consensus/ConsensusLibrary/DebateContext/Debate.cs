﻿using System;
using System.Collections.Generic;
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
        private List<DebateMember> _members { get; }
    }
}