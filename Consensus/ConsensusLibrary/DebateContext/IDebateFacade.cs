using System;
using System.Collections.Generic;
using ConsensusLibrary.CategoryContext;
using ConsensusLibrary.DebateContext.Views;
using ConsensusLibrary.Tools;

namespace ConsensusLibrary.DebateContext
{
    public interface IDebateFacade
    {
        Identifier CreateDebate(DateTimeOffset startDateTime,
            string title, Identifier leftOpponent, Identifier rightOpponent, string debateCategory);
        DebateView GetDebate(Identifier identifier);
        IEnumerable<LiveDebateView> GetLiveDebates();
        void SetReadyStatus(Identifier debateId, Identifier userId);
    }
}