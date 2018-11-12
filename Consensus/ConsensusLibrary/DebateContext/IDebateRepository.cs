using System;
using System.Collections.Generic;
using ConsensusLibrary.Tools;

namespace ConsensusLibrary.DebateContext
{
    public interface IDebateRepository
    {
        void AddDebate(Debate newDebate);
        Debate GetDebate(Identifier identifier);
        IEnumerable<Debate> GetDebatesForUser(Identifier identifier);
        IEnumerable<Debate> GetActualDebates();
        void UpdateDebate(Debate newDebate);
    }
}