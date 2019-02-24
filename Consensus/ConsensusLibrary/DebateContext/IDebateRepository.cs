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
        IEnumerable<Debate> SearchDebate(
            string debateTitle,
            string category,
            bool isLive,
            int pageSize,
            int pageNumber);
        void UpdateDebate(Debate newDebate);
    }
}