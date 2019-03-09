using ConsensusLibrary.Tools;
using ConsensusLibrary.UserContext.Views;

namespace ConsensusLibrary.UserContext
{
    public interface IUserProfileFacade
    {
        LoginUserProfileView GetUserProfileForLogin(Identifier userId);
        GetUserProfileView GetUserProfile(Identifier userId);

        void ChangeUserAbout(Identifier userId, string newAbout);
        void ChangeUserAvatar(Identifier userId, string newAvatar);
        void ChangeUserEmail(Identifier userId, string newEmail);
        void ChangeUserName(Identifier userId, string newName);
        void ChangeUserPassword(Identifier userId, string newPassword);
    }
}