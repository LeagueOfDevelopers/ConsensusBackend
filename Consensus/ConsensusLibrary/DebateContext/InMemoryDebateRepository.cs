using System.Collections.Generic;
using System.Linq;
using ConsensusLibrary.Tools;
using EnsureThat;
using ConsensusLibrary.DebateContext.Exceptions;

namespace ConsensusLibrary.DebateContext
{
    public class InMemoryDebateRepository : IDebateRepository
    {
        public InMemoryDebateRepository()
        {
            _debates = new List<Debate>();
        }

        public void AddDebate(Debate newDebate)
        {
            Ensure.Any.IsNotNull(newDebate);

            _debates.Add(newDebate);
        }

        public Debate GetDebate(Identifier identifier)
        {
            Ensure.Any.IsNotNull(identifier);

            var expectedDebate = _debates.FirstOrDefault(d => d.Identifier.Id == identifier.Id);
            Ensure.Any.IsNotNull(expectedDebate, nameof(expectedDebate), opt => opt.WithException(new DebateNotFoundException(identifier)));

            return expectedDebate;
        }

        public IEnumerable<Debate> GetDebatesForUser(Identifier identifier)
        {
            Ensure.Any.IsNotNull(identifier);

            var debates = _debates.Where(d => d.Members.Any(m => m.UserIdentifier.Id == identifier.Id));

            return debates;
        }

        public void UpdateDebate(Debate newDebate)
        {
            
        }

        private readonly List<Debate> _debates;
    }
}
