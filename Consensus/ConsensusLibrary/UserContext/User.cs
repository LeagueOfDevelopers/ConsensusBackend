using System;
using ConsensusLibrary.Tools;
using EnsureThat;

namespace ConsensusLibrary.UserContext
{
    public class User
    {
        public User(string email, string nickName, string passwordHash)
        {
            Ensure.String.IsNotNullOrWhiteSpace(email);
            Ensure.String.IsNotNullOrWhiteSpace(nickName);
            Ensure.String.IsNotNullOrWhiteSpace(passwordHash);

            Identifier = new Identifier();
            Credentials = new Credentials(passwordHash, nickName, email);
            UserProfile = new UserProfile(DateTimeOffset.Now);
        }

        internal User(
            Credentials credentials,
            Identifier identifier,
            UserProfile userProfile)
        {
            Credentials = Ensure.Any.IsNotNull(credentials);
            Identifier = Ensure.Any.IsNotNull(identifier);
            UserProfile = Ensure.Any.IsNotNull(userProfile);
        }

        public Credentials Credentials { get; }
        public Identifier Identifier { get; }
        public UserProfile UserProfile { get; }
    }
}