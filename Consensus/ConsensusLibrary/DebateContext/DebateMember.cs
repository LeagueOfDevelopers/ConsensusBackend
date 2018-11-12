using ConsensusLibrary.Tools;

namespace ConsensusLibrary.DebateContext
{
    public class DebateMember
    {
        public DebateMember(
            MemberRole memberRole,
            Identifier userIdentifier,
            string translationLink)
        {
            MemberRole = memberRole;
            UserIdentifier = userIdentifier;
            TranslationLink = translationLink;
            Ready = false;
        }

        public MemberRole MemberRole { get; }
        public Identifier UserIdentifier { get; }
        public string TranslationLink { get; }
        public bool Ready { get; private set; }

        internal void BecomeReady()
        {
            Ready = true;
        }
    }
}