using System;
using System.Collections.Generic;
using System.Linq;
using ConsensusLibrary.DebateContext.Exceptions;
using ConsensusLibrary.Tools;
using EnsureThat;

namespace ConsensusLibrary.DebateContext
{
    public class InMemoryDebateRepository : IDebateRepository
    {
        private readonly List<Debate> _debates;

        public InMemoryDebateRepository()
        {
            _debates = new List<Debate>();
        }

        public void AddDebate(Debate newDebate)
        {
            Ensure.Any.IsNotNull(newDebate);

            _debates.Add(newDebate);
        }

        public IEnumerable<Debate> GetActualDebatesForInterval(int minutesLater)
        {
            Ensure.Any.IsNotDefault(minutesLater);
            return _debates.Where(d => d.StartDateTime > DateTimeOffset.UtcNow && d.StartDateTime.AddMinutes(minutesLater) < DateTimeOffset.UtcNow);
        }

        public Debate GetDebate(Identifier identifier)
        {
            Ensure.Any.IsNotNull(identifier);

            var expectedDebate = _debates.FirstOrDefault(d => d.Identifier.Id == identifier.Id);
            Ensure.Any.IsNotNull(expectedDebate, nameof(expectedDebate),
                opt => opt.WithException(new DebateNotFoundException(identifier)));

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
    }
}