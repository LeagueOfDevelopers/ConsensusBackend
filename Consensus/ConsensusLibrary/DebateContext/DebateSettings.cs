using System;

namespace ConsensusLibrary.DebateContext
{
    public class DebateSettings
    {
        public DebateSettings(
            int roundCount,
            TimeSpan roundLength)
        {
            RoundCount = roundCount;
            RoundLength = roundLength;
        }

        public int RoundCount { get; }
        public TimeSpan RoundLength { get; }
    }
}