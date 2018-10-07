using ConsensusLibrary.Tools;
using ConsensusLibrary.DebateContext.Views;
using System;

namespace ConsensusLibrary.DebateContext
{
    public interface IDebateFacade
    {
        Identifier CreateDebate(DateTimeOffset startDateTime, DateTimeOffset endDateTime,
            string title, Identifier leftOpponent, Identifier rightOpponent, DebateCategory debateCategory);
        DebateView GetDebate(Identifier identifier);
    }
}
