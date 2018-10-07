using ConsensusLibrary.Tools;
using System;

namespace ConsensusLibrary.DebateContext.Views
{
    public class DebateView
    {
        public Identifier Identifier { get; }
        public string LeftFighterNickName { get; }
        public Identifier LeftFighterId { get; }
        public string RightFighterNickName { get; }
        public Identifier RightFighterId { get; }
        public DateTimeOffset StartDateTime { get; }
        public DateTimeOffset EndDateTime { get; }
        public int ViewerCount { get; }
        public string Title { get; }
        public DebateCategory Category { get; }

        public DebateView(
            Identifier identifier,
            string leftFighterNickName,
            Identifier leftFighterId,
            string rightFighterNickName,
            Identifier rightFighterId,
            DateTimeOffset startDateTime,
            DateTimeOffset endDateTime,
            int viewerCount,
            string title,
            DebateCategory category)
        {
            Identifier = identifier;
            LeftFighterNickName = leftFighterNickName;
            LeftFighterId = leftFighterId;
            RightFighterNickName = rightFighterNickName;
            RightFighterId = rightFighterId;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            ViewerCount = viewerCount;
            Title = title;
            Category = category;
        }
    }
}
