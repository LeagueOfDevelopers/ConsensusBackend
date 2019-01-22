using ConsensusLibrary.Tools;
using ConsensusLibrary.UserContext.Views;

namespace ConsensusLibrary.UserContext
{
    public interface IUserProfileFacade
    {
        LoginUserProfileView GetUserProfileForLogin(Identifier userId);
    }
}