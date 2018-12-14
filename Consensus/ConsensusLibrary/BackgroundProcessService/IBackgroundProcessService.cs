using System;
using ConsensusLibrary.DebateContext;
using ConsensusLibrary.Tools;

namespace ConsensusLibrary.BackgroundProcessService
{
    public interface IBackgroundProcessService
    {
        void DelayedCheckDebateOverdue(Identifier debate, DateTimeOffset startDebate);
        void DelayedDebateChangingProcess(Debate debate);
    }
}
