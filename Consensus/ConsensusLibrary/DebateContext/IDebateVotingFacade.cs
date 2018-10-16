using ConsensusLibrary.DebateContext.Views;
using ConsensusLibrary.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsensusLibrary.DebateContext
{
    public interface IDebateVotingFacade
    {
        void Vote(Identifier debate, Identifier fromUser, Identifier toUser);
        DebateVotingView GetVotingResults(Identifier debateId);
    }
}
