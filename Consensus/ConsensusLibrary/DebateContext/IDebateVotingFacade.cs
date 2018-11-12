using ConsensusLibrary.DebateContext.Views;
using ConsensusLibrary.Tools;

namespace ConsensusLibrary.DebateContext
{
    public interface IDebateVotingFacade
    {
        void Vote(Identifier debate, Identifier fromUser, Identifier toUser);
        DebateVotingView GetVotingResults(Identifier debateId);
    }
}