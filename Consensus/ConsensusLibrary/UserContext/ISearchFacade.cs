using ConsensusLibrary.UserContext.Views;

namespace ConsensusLibrary.UserContext
{
    public interface IUserSearchFacade
    {
        SearchUserByNameView SearchUserByName(string nameSection);
    }
}
