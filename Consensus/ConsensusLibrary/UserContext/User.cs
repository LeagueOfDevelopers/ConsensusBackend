using ConsensusLibrary.Tools;
using EnsureThat;

namespace ConsensusLibrary.UserContext
{
    public class User
    {
        public Credentials Credentials { get; private set; }
        public Identifier Identifier { get; private set; }
        public UserProfile UserProfile { get; private set; }

        public User(string email, string nickName, string passwordHash)
        {
            Ensure.String.IsNotNullOrWhiteSpace(email);
            Ensure.String.IsNotNullOrWhiteSpace(nickName);
            Ensure.String.IsNotNullOrWhiteSpace(passwordHash);

            Identifier = new Identifier();
            Credentials = new Credentials(passwordHash, nickName, email);
        }
    }
}
