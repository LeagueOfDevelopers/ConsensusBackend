using System;
using System.Collections.Generic;
using System.Linq;
using ConsensusLibrary.Tools;
using ConsensusLibrary.UserContext.Exceptions;
using EnsureThat;

namespace ConsensusLibrary.UserContext
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users;

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

            email = email.ToLower();
            nickName = nickName.ToLower();

            var result = _users.FirstOrDefault(u =>
                u.Credentials.Email.ToLower() == email || u.Credentials.NickName.ToLower() == nickName);

            return result;
            ;
        }

        public IEnumerable<User> GetUsersByName(string nameSection)
        {
            Ensure.String.IsNotNullOrWhiteSpace(nameSection);

            nameSection = nameSection.ToLower();

            var result = _users.Where(u => u.Credentials.NickName
                .ToLower()
                .Contains(nameSection))
                .ToList()
                .OrderBy(u => u.Credentials.NickName.Length);

            return result;
        }
    }
}