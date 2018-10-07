using System;

namespace ConsensusLibrary.Tools
{
    public class Identifier
    {
        public Guid Id { get; }

        internal Identifier()
        {
            Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
