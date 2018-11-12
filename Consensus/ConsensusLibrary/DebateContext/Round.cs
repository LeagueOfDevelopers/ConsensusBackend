using System;
using ConsensusLibrary.Tools;
using EnsureThat;

namespace ConsensusLibrary.DebateContext
{
    public class Round
    {
        public Round(DateTimeOffset startRoundDatetime, DateTimeOffset endRoundDateTime, Identifier speakerId)
        {
            StartRoundDatetime = Ensure.Any.IsNotDefault(startRoundDatetime);
            EndRoundDateTime = Ensure.Any.IsNotDefault(endRoundDateTime);
            SpeakerId = Ensure.Any.IsNotNull(speakerId);
        }

        public DateTimeOffset StartRoundDatetime { get; }
        public DateTimeOffset EndRoundDateTime { get; }
        public Identifier SpeakerId { get; }
    }
}