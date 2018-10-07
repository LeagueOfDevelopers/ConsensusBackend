using System;
using EnsureThat;

namespace ConsensusLibrary.Tools
{
    public class Identifier
    {
        public Guid Id { get; }

        internal Identifier()
        {
            Id = Guid.NewGuid();
        }

        public Identifier(Guid id)
        {
            Id = Ensure.Guid.IsNotEmpty(id);
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
