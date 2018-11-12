using ConsensusLibrary.Tools;

namespace ConsensusLibrary.DebateContext
{
    public class VoteInfo
    {
        public VoteInfo(Identifier fromUser, Identifier toUser)
        {
            FromUser = fromUser;
            ToUser = toUser;
        }

        public Identifier FromUser { get; }
        public Identifier ToUser { get; }
    }
}