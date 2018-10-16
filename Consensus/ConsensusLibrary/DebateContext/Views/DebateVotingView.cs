using System;

namespace ConsensusLibrary.DebateContext.Views
{
    public class DebateVotingView
    {
        public int FirstDedaterVotesTotalCount { get; }
        public string FirstDedaterNickName { get; }
        public Guid FirstDedaterIdentifier { get; }

        public int SecondDedaterVotesTotalCount { get; }
        public string SecondDedaterNickName { get; }
        public Guid SecondDedaterIdentifier { get; }

        public DebateVotingView(
            int firstDedaterVotesTotalCount,
            string firstDedaterNickName, 
            Guid firstDedaterIdentifier, 
            int secondDedaterVotesTotalCount, 
            string secondDedaterNickName, 
            Guid secondDedaterIdentifier)
        {
            FirstDedaterVotesTotalCount = firstDedaterVotesTotalCount;
            FirstDedaterNickName = firstDedaterNickName;
            FirstDedaterIdentifier = firstDedaterIdentifier;
            SecondDedaterVotesTotalCount = secondDedaterVotesTotalCount;
            SecondDedaterNickName = secondDedaterNickName;
            SecondDedaterIdentifier = secondDedaterIdentifier;
        }
    }
}
