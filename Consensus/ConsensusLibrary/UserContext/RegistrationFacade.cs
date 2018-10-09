using ConsensusLibrary.CryptoContext;
using EnsureThat;
using System;
using ConsensusLibrary.UserContext.Exceptions;
using ConsensusLibrary.Tools;
using ConsensusLibrary.UserContext.Views;

namespace ConsensusLibrary.UserContext
{
    public class RegistrationFacade : IRegistrationFacade
    {
        public RegistrationFacade(
            IUserRepository userRepository,
            ICryptoService cryptoService)
        {
            _userRepository = Ensure.Any.IsNotNull(userRepository);
            _cryptoService = Ensure.Any.IsNotNull(cryptoService);
        }

        public CheckUserExistenceView CheckUserExistence(string email, string password)
        {
            Ensure.String.IsNotNullOrWhiteSpace(email, nameof(email), opt => opt.WithException(new ArgumentNullException()));
            Ensure.String.IsNotNullOrWhiteSpace(password, nameof(password), opt => opt.WithException(new ArgumentNullException()));

            email = email.ToLower().Replace(" ", "");

            var user = _userRepository.TryGetUserByEmail(email);

            if (user != null && _cryptoService.CompareHashWithString(user.Credentials.PasswordHash, password))
                return new CheckUserExistenceView(user.Identifier);

            return null;
        }

        public Identifier RegisterUser(string email, string nickName, string password)
        {
            Ensure.String.IsNotNullOrWhiteSpace(email, nameof(email), opt => opt.WithException(new ArgumentNullException()));
            Ensure.String.IsNotNullOrWhiteSpace(nickName, nameof(email), opt => opt.WithException(new ArgumentNullException()));
            Ensure.String.IsNotNullOrWhiteSpace(password, nameof(email), opt => opt.WithException(new ArgumentNullException()));

            email = email.ToLower().Replace(" ", "");
            nickName = nickName.Replace(" ", "");

            var possibleClone = _userRepository.TryGetUserByEmailOrNickName(email, nickName);
            Ensure.Bool.IsTrue(possibleClone == null, nameof(possibleClone), opt => opt.WithException(new UserAlreadyExistsException()));

            var newUser = new User(email, nickName, _cryptoService.CalculateHash(password));

            _userRepository.AddUser(newUser);

            return newUser.Identifier;
        }

        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;

    }
}
