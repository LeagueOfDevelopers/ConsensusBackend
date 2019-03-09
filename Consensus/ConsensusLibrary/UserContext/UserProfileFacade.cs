using ConsensusLibrary.CryptoContext;
using ConsensusLibrary.FileContext;
using ConsensusLibrary.Tools;
using ConsensusLibrary.UserContext.Exceptions;
using ConsensusLibrary.UserContext.Views;
using EnsureThat;

namespace ConsensusLibrary.UserContext
{
    public class UserProfileFacade : IUserProfileFacade
    {
        public LoginUserProfileView GetUserProfileForLogin(Identifier userId)
        {
            Ensure.Any.IsNotNull(userId);

            var currentUser = _userRepository.GetUserById(userId);

            var result = new LoginUserProfileView(currentUser.Credentials.NickName,
                currentUser.Credentials.Email, currentUser.UserProfile.RegistrationDate, currentUser.UserProfile.AvatarLink);

            return result;
        }

        public GetUserProfileView GetUserProfile(Identifier userId)
        {
            Ensure.Any.IsNotNull(userId);

            var currentUser = _userRepository.GetUserById(userId);

            var result = new GetUserProfileView(
                currentUser.Credentials.NickName, currentUser.UserProfile.AvatarLink, currentUser.UserProfile.About,
                currentUser.UserProfile.Reputation, currentUser.Credentials.Email);

            return result;
        }

        public void ChangeUserAbout(
            Identifier userId,
            string newAbout)
        {
            Ensure.Any.IsNotNull(userId);
            Ensure.String.IsNotNullOrWhiteSpace(newAbout);

            var currentUser = _userRepository.GetUserById(userId);

            currentUser.UserProfile.About = newAbout;

            _userRepository.UpdateUser(currentUser);
        }

        public void ChangeUserAvatar(
            Identifier userId,
            string newAvatar)
        {
            Ensure.Any.IsNotNull(userId);
            Ensure.String.IsNotNullOrWhiteSpace(newAvatar);

            var currentUser = _userRepository.GetUserById(userId);

            _fileRepository.GetFile(newAvatar);

            currentUser.UserProfile.AvatarLink = newAvatar;

            _userRepository.UpdateUser(currentUser);
        }

        public void ChangeUserEmail(
            Identifier userId,
            string newEmail)
        {
            Ensure.Any.IsNotNull(userId);
            //TODO check with regex
            Ensure.String.IsNotNullOrWhiteSpace(newEmail);

            var currentUser = _userRepository.GetUserById(userId);

            Ensure.Bool.IsTrue(_userRepository.TryGetUserByEmail(newEmail) == null,
                nameof(newEmail),
                opt => opt.WithException(new UserAlreadyExistsException()));

            currentUser.Credentials.Email = newEmail;

            _userRepository.UpdateUser(currentUser);
        }

        public void ChangeUserName(
            Identifier userId,
            string newName)
        {
            Ensure.Any.IsNotNull(userId);
            //TODO check for some rules
            Ensure.String.IsNotNullOrWhiteSpace(newName);

            var currentUser = _userRepository.GetUserById(userId);

            Ensure.Bool.IsTrue(_userRepository.TryGetUserByEmailOrNickName(string.Empty, newName) == null,
                nameof(newName), opt => opt.WithException(new UserAlreadyExistsException()));

            currentUser.Credentials.NickName = newName;

            _userRepository.UpdateUser(currentUser);
        }

        public void ChangeUserPassword(
            Identifier userId,
            string newPassword)
        {
            Ensure.Any.IsNotNull(userId);
            //TODO check for some rules
            Ensure.String.IsNotNullOrWhiteSpace(newPassword);

            var currentUser = _userRepository.GetUserById(userId);

            currentUser.Credentials.PasswordHash = _cryptoService.CalculateHash(newPassword);

            _userRepository.UpdateUser(currentUser);
        }

        private readonly IUserRepository _userRepository;
        private readonly IFileRepository _fileRepository;
        private readonly ICryptoService _cryptoService;

        public UserProfileFacade(
            IUserRepository userRepository,
            IFileRepository fileRepository,
            ICryptoService cryptoService)
        {
            _userRepository = Ensure.Any.IsNotNull(userRepository);
            _fileRepository = Ensure.Any.IsNotNull(fileRepository);
            _cryptoService = Ensure.Any.IsNotNull(cryptoService);
        }
    }
}
