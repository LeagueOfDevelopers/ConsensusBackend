using ConsensusLibrary.Tools;

namespace ConsensusLibrary.DebateContext
{
    public class VoteInfo
    {
        public Identifier FromUser { get; }
        public Identifier ToUser { get; }

        public VoteInfo(Identifier fromUser, Identifier toUser)
        {
            FromUser = fromUser;
            ToUser = toUser;
        }
    }
}
