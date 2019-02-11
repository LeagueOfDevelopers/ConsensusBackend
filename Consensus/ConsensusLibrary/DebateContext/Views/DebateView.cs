using System;
using System.Collections.Generic;
using ConsensusLibrary.Tools;

namespace ConsensusLibrary.DebateContext.Views
{
    public class DebateView
    {
        public DebateView(
            Identifier identifier,
            DateTimeOffset startDateTime,
            string title,
            string category,
            DebateState state,
            IEnumerable<DebateMemberView> members)
        {
            Identifier = identifier;
            StartDateTime = startDateTime;
            Title = title;
            Category = category;
            State = state;
            Members = members;
        }

        public Identifier Identifier { get; }
        public DateTimeOffset StartDateTime { get; }
        public string Title { get; }
        public string Category { get; }
        public DebateState State { get; }
        public IEnumerable<DebateMemberView> Members { get; }
    }
}