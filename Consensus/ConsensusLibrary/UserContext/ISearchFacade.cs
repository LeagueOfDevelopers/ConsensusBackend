using ConsensusLibrary.UserContext.Views;

namespace ConsensusLibrary.UserContext
{
    public interface IUserSearchFacade
    {
        SearchUserByNameView SearchUserByName(string nameSection);

        SearchUsersAndDebatesView SearchUsersAndDebates(
            string sectionName,
            string category,
            bool isLive,
            int pageSize,
            int debatePageNumber,
            int userPageNumber);
    }
}
