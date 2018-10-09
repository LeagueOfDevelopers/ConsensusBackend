using ConsensusLibrary.Tools;

namespace ConsensusLibrary.UserContext.Views
{
    public class CheckUserExistenceView
    {
        public CheckUserExistenceView(Identifier identifier)
        {
            Identifier = identifier;
        }

        public Identifier Identifier { get; }
    }
}