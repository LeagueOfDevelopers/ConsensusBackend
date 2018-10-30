using System.Collections.Generic;
using ConsensusLibrary.DebateContext.Views;
using ConsensusLibrary.Tools;

namespace ConsensusLibrary.DebateContext
{
    public interface IChatFacade
    {
        Identifier SendMessage(Identifier userId, Identifier debateId, string text);
        IEnumerable<MessageView> GetMessages(Identifier debateId);
    }
}
