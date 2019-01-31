using ConsensusLibrary.Tools;

namespace ConsensusLibrary.UserContext.Views
{
    public class SearchUserByNameItemView
    {
        public string UserName { get; }
        public Identifier UserIdentifier { get; }

        public SearchUserByNameItemView(string userName, Identifier userIdentifier)
        {
            UserName = userName;
            UserIdentifier = userIdentifier;
        }
    }
}
