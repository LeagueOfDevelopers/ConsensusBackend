using ConsensusLibrary.Tools;
using ConsensusLibrary.UserContext.Views;
using EnsureThat;

namespace ConsensusLibrary.UserContext
{
    public class UserProfileFacade : IUserProfileFacade
    {
        public UserProfileFacade(IUserRepository userRepository)
        {
            _userRepository = Ensure.Any.IsNotNull(userRepository);
        }

        public LoginUserProfileView GetUserProfileForLogin(Identifier userId)
        {
            var currentUser = _userRepository.GetUserById(userId);

            var result = new LoginUserProfileView(currentUser.Credentials.NickName,
                currentUser.Credentials.Email, currentUser.UserProfile.RegistrationDate);

            return result;
        }

        private readonly IUserRepository _userRepository;
    }
}
