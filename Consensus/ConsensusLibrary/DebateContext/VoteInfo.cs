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

        public override bool Equals(object obj)
        {
            return obj is VoteInfo info &&
                   FromUser.Id == info.FromUser.Id &&
                   ToUser.Id == info.ToUser.Id;
        }
    }
}