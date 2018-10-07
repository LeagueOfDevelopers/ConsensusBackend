using System;
using ConsensusLibrary.Tools;

namespace ConsensusLibrary.DebateContext.Exceptions
{
    public class DebateNotFoundException : Exception
    {
        public DebateNotFoundException(Identifier identifier) : base($"Debate with id {identifier.ToString()} not found")
        {
        }
    }
}
