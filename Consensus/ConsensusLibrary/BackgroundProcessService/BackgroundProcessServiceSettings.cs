using System;

namespace ConsensusLibrary.BackgroundProcessService
{
    public class BackgroundProcessServiceSettings
    {
        public TimeSpan AllowedOverdueForDebateStart { get; }
        public TimeSpan RoundLength { get; }

        public BackgroundProcessServiceSettings(
            TimeSpan allowedOverdueForDebateStart,
            TimeSpan roundLength)
        {
            AllowedOverdueForDebateStart = allowedOverdueForDebateStart;
            RoundLength = roundLength;
        }
    }
}
