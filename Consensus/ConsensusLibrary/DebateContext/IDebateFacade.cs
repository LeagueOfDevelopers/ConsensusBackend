using System;
using ConsensusLibrary.DebateContext.Views;
using ConsensusLibrary.Tools;

namespace ConsensusLibrary.DebateContext
{
    public interface IDebateFacade
    {
        Identifier CreateDebate(DateTimeOffset startDateTime, DateTimeOffset endDateTime,
            string title, Identifier leftOpponent, Identifier rightOpponent, DebateCategory debateCategory);

        DebateView GetDebate(Identifier identifier);
    }
}