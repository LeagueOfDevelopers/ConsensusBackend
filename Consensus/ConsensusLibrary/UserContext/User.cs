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
        }

        public Credentials Credentials { get; }
        public Identifier Identifier { get; }
        public UserProfile UserProfile { get; private set; }
    }
}