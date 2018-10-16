using System;
using ConsensusLibrary.Tools;

namespace ConsensusLibrary.DebateContext.Views
{
    public class DebateView
    {
        public DebateView(
            Identifier identifier,
            string leftFighterNickName,
            Identifier leftFighterId,
            string rightFighterNickName,
            Identifier rightFighterId,
            DateTimeOffset startDateTime,
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
            ViewerCount = viewerCount;
            Title = title;
            Category = category;
        }

        public Identifier Identifier { get; }
        public string LeftFighterNickName { get; }
        public Identifier LeftFighterId { get; }
        public string RightFighterNickName { get; }
        public Identifier RightFighterId { get; }
        public DateTimeOffset StartDateTime { get; }
        public int ViewerCount { get; }
        public string Title { get; }
        public DebateCategory Category { get; }
    }
}