using ConsensusLibrary.Tools;

namespace ConsensusLibrary.UserContext.Views
{
    public class SearchUserByNameItemView
    {
        public string UserName { get; }
        public Identifier UserIdentifier { get; }
        public string Avatar { get; }

        public SearchUserByNameItemView(
            string userName, 
            Identifier userIdentifier, 
            string avatar)
        {
            UserName = userName;
            UserIdentifier = userIdentifier;
            Avatar = avatar;
        }
    }
}
