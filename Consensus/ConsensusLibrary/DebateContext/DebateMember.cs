using ConsensusLibrary.Tools;

namespace ConsensusLibrary.DebateContext
{
    public class DebateMember
    {
        public MemberRole MemberRole { get; set; }
        public Identifier UserIdentifier{ get; set; }
        public string TraslationLink { get; set; }

        public DebateMember(
            MemberRole memberRole,
            Identifier userIdentifier,
            string traslationLink)
        {
            MemberRole = memberRole;
            UserIdentifier = userIdentifier;
            TraslationLink = traslationLink;
        }
    }
}
