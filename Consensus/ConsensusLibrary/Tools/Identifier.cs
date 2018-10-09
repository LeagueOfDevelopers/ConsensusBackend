using System;
using EnsureThat;

namespace ConsensusLibrary.Tools
{
    public class Identifier
    {
        internal Identifier()
        {
            Id = Guid.NewGuid();
        }

        public Identifier(Guid id)
        {
            Id = Ensure.Guid.IsNotEmpty(id);
        }

        public Guid Id { get; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}