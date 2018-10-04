using EnsureThat;

namespace ConsensusLibrary.UserContext
{
    public class Credentials
    {
        public string PasswordHash { get; }
        public string NickName { get; }
        public string Email { get; }

        public Credentials(
            string passwordHash,
            string nickName,
            string email)
        {
            PasswordHash = Ensure.String.IsNotNullOrEmpty(passwordHash);
            NickName = Ensure.String.IsNotNullOrEmpty(nickName);
            Email = Ensure.String.IsNotNullOrWhiteSpace(email);
        }
    }
}
