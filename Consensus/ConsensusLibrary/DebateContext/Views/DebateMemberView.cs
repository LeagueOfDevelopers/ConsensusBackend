using ConsensusLibrary.Tools;

namespace ConsensusLibrary.DebateContext.Views
{
    public class DebateMemberView
    {
        public DebateMemberView(
            string nickName,
            Identifier identifier,
            bool ready,
            string translationLink)
        {
            NickName = nickName;
            Identifier = identifier;
            Ready = ready;
            TranslationLink = translationLink;
        }

        public string NickName { get; }
        public Identifier Identifier { get; }
        public bool Ready { get; }
        public string TranslationLink { get; }
    }
}