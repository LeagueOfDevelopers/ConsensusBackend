using ConsensusLibrary.UserContext.Views;

namespace ConsensusLibrary.UserContext
{
    public interface IUserSearchFacade
    {
        SearchUsersAndDebatesView SearchUsersAndDebates(
            string sectionName,
            string category,
            bool isLive,
            int pageSize,
            int debatePageNumber,
            int userPageNumber);
        SearchUserByNameView SearchUserByName(string nameSection, int pageSize, int pageNumber);
    }
}
