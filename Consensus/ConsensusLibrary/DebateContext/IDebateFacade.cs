﻿using System;
using System.Collections.Generic;
using ConsensusLibrary.DebateContext.Views;
using ConsensusLibrary.Tools;

namespace ConsensusLibrary.DebateContext
{
    public interface IDebateFacade
    {
        Identifier CreateDebate(DateTimeOffset startDateTime,
            string title, Identifier leftOpponent, Identifier rightOpponent, DebateCategory debateCategory);

        DebateView GetDebate(Identifier identifier);
        IEnumerable<LiveDebateView> GetLiveDebates();
    }
}