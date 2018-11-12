using System;

namespace ConsensusLibrary.DebateContext
{
    public class DebateSettings
    {
        public int RoundCount { get; }
        public TimeSpan RoundLength { get; }

        public DebateSettings(
            int roundCount,
            TimeSpan roundLength)
        {
            RoundCount = roundCount;
            RoundLength = roundLength;
        }
    }
}
