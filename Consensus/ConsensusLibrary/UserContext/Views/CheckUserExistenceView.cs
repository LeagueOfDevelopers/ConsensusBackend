using ConsensusLibrary.Tools;

namespace ConsensusLibrary.UserContext.Views
{
    public class CheckUserExistenceView
    {
        public Identifier Identifier { get; }

        public CheckUserExistenceView(Identifier identifier)
        {
            Identifier = identifier;
        }
    }
}
