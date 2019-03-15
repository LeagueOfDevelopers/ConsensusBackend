using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ConsensusLibrary.Tools;
using ConsensusLibrary.UserContext.Exceptions;
using Dapper;
using EnsureThat;
using Npgsql;

namespace ConsensusLibrary.UserContext
{
    public class InPostgreSqlUserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            Ensure.Any.IsNotNull(user);

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string sqlQuery = "INSERT INTO users (id, nickname, email," +
                               " passwordhash, registrationdate) VALUES (@Id, @Nickname, @Email," +
                               " @PasswordHash, @RegistrationDate)";

                var userDto = new
                {
                    Id = user.Identifier.Id,
                    Nickname = user.Credentials.NickName,
                    Email = user.Credentials.Email,
                    PasswordHash = user.Credentials.PasswordHash,
                    RegistrationDate = user.UserProfile.RegistrationDate
                };

                connection.Execute(sqlQuery, userDto);
            }
        }

        public void UpdateUser(User user)
        {
            Ensure.Any.IsNotNull(user);

            using (IDbConnection connection = new NpgsqlConnection(_connectionString)) {
                const string sqlQuery = "UPDATE users SET nickname = @Nickname, " +
                                        "email = @Email, passwordHash = @PasswordHash, " +
                                        "about = @About, avatarlink = @AvatarLink, " +
                                        "reputation = @Reputation, registrationDate = @RegistrationDate";
                var userDto = new
                {
                    Nickname = user.Credentials.NickName,
                    Email = user.Credentials.Email,
                    PasswordHash = user.Credentials.PasswordHash,
                    About = user.UserProfile.About,
                    AvatarLink = user.UserProfile.AvatarLink,
                    Reputation = user.UserProfile.Reputation,
                    RegistrationDate = user.UserProfile.RegistrationDate
                };

                connection.Execute(sqlQuery, userDto);
            }
        }

        public User GetUserById(Identifier identifier)
        {
            Ensure.Any.IsNotNull(identifier);

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string query = "SELECT id, nickname, email, passwordhash, about, avatarlink, " +
                                     "reputation, registrationdate FROM users WHERE id = @Id";

                var currentUser = connection.QueryFirstOrDefault(query, new {Id = identifier.Id});

                //TODO crap code
                if (currentUser == null)
                    throw new UserNotFoundException();

                var result = new User(
                    new Credentials(currentUser.passwordhash, currentUser.nickname, currentUser.email),
                    new Identifier(currentUser.id),
                    new UserProfile(currentUser.about, currentUser.avatarlink,
                        currentUser.reputation, currentUser.registrationdate));

                return result;
            }
        }

        public User GetUserByCredentials(Credentials credentials)
        {
            Ensure.Any.IsNotNull(credentials);

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string query = "SELECT id, nickname, email, passwordhash, about, avatarlink, " +
                                     "reputation, registrationdate FROM users " +
                                     "WHERE passwordhash = @PasswordHash AND " +
                                     "email = @Email";

                var currentUser = connection.QueryFirstOrDefault(query,
                    new
                    {
                        PasswordHash = credentials.PasswordHash,
                        Email = credentials.Email
                    });

                //TODO crap code
                if (currentUser == null)
                    throw new UserNotFoundException();

                var result = new User(
                    new Credentials(currentUser.passwordhash, currentUser.nickname, currentUser.email),
                    new Identifier(currentUser.id),
                    new UserProfile(currentUser.about, currentUser.avatarlink,
                        currentUser.reputation, currentUser.registrationdate));

                return result;
            }
        }

        public User TryGetUserByEmail(string email)
        {
            Ensure.String.IsNotNullOrWhiteSpace(email);

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string query = "SELECT id, nickname, email, passwordhash, about, avatarlink, " +
                                     "reputation, registrationdate FROM users " +
                                     "WHERE email = @Email";

                var currentUser = connection.QueryFirstOrDefault(query,
                    new
                    {
                        Email = email
                    });

                //TODO crap code
                if (currentUser == null)
                    return null;

                var result = new User(
                    new Credentials(currentUser.passwordhash, currentUser.nickname, currentUser.email),
                    new Identifier(currentUser.id),
                    new UserProfile(currentUser.about, currentUser.avatarlink,
                        currentUser.reputation, currentUser.registrationdate));

                return result;
            }
        }

        public User TryGetUserByEmailOrNickName(string email, string nickName)
        {
            Ensure.Any.IsNotNull(email);
            Ensure.Any.IsNotNull(nickName);

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string query = "SELECT id, nickname, email, passwordhash, about, avatarlink, " +
                                     "reputation, registrationdate FROM users " +
                                     "WHERE email = @Email OR nickname = @NickName";

                var currentUser = connection.QueryFirstOrDefault(query,
                    new
                    {
                        Email = email,
                        NickName = nickName
                    });

                //TODO crap code
                if (currentUser == null)
                    return null;

                var result = new User(
                    new Credentials(currentUser.passwordhash, currentUser.nickname, currentUser.email),
                    new Identifier(currentUser.id),
                    new UserProfile(currentUser.about, currentUser.avatarlink,
                        currentUser.reputation, currentUser.registrationdate));

                return result;
            }
        }

        public IEnumerable<User> GetUsersByName(string nameSection, int pageSize, int pageNumber)
        {
            Ensure.String.IsNotNullOrWhiteSpace(nameSection);

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                nameSection = nameSection.Replace("[", "[[]").Replace("%", "[%]");
                var nameForLike = $"%{nameSection}%";

                const string query = "SELECT id, nickname, email, passwordhash, about, avatarlink, " +
                                     "reputation, registrationdate FROM users " +
                                     "WHERE nickname LIKE @NameSection LIMIT @PageSize " +
                                     "OFFSET @Offset";

                var selectedUsers = connection.Query(query,
                    new
                    {
                        NameSection = nameForLike,
                        PageSize = pageSize,
                        Offset = pageSize*(pageNumber-1)
                    });

                var userList = selectedUsers.ToList();

                var result = new List<User>();

                userList.ForEach(u => result.Add(new User
                    (
                    new Credentials(u.passwordhash, u.nickname, u.email), 
                    new Identifier(u.id),
                    new UserProfile(u.about, u.avatarlink, u.reputation, u.registrationdate)
                    )));

                return result;
            }
        }

        public InPostgreSqlUserRepository(string connectionString)
        {
            _connectionString = Ensure.String.IsNotNullOrWhiteSpace(connectionString);
        }

        private readonly string _connectionString;
    }
}
