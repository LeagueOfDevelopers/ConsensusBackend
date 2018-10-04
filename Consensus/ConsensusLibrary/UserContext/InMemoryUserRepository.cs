using ConsensusLibrary.Tools;
using System.Collections.Generic;
using EnsureThat;
using System.Linq;
using ConsensusLibrary.UserContext.Exceptions;

namespace ConsensusLibrary.UserContext
{
    public class InMemoryUserRepository : IUserRepository
    {
        public InMemoryUserRepository()
        {
            _users = new List<User>();

        }

        public void AddUser(User user)
        {
            Ensure.Any.IsNotNull(user);
            _users.Add(user);
        }

        public User GetUserByCredentials(Credentials credentials)
        {
            var result = _users.FirstOrDefault(u => u.Credentials.Email == credentials.Email
                && u.Credentials.PasswordHash == credentials.PasswordHash);

            Ensure.Any.IsNotNull(result, nameof(result), opt => opt.WithException(new UserNotFoundException()));

            return result;
        }

        public User GetUserById(Identifier identifier)
        {
            var result = _users.FirstOrDefault(u => u.Identifier.Id == identifier.Id);

            Ensure.Any.IsNotNull(result, nameof(result), opt => opt.WithException(new UserNotFoundException()));

            return result;
        }

        public void UpdateUser(User user)
        {

        }

        public User TryGetUserByEmail(string email)
        {
            Ensure.String.IsNotNullOrWhiteSpace(email);

            var result = _users.FirstOrDefault(u => u.Credentials.Email == email);

            return result;
        }

        public User TryGetUserByEmailOrNickName(string email, string nickName)
        {
            Ensure.String.IsNotNullOrWhiteSpace(email);
            Ensure.String.IsNotNullOrWhiteSpace(nickName);

            var result = _users.FirstOrDefault(u => u.Credentials.Email == email || u.Credentials.NickName == nickName);

            return result; ;
        }

        private readonly List<User> _users;
    }
}
