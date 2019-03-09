using EnsureThat;

namespace ConsensusLibrary.UserContext
{
    public class Credentials
    {
        public Credentials(
            string passwordHash,
            string nickName,
            string email)
        {
            PasswordHash = Ensure.String.IsNotNullOrEmpty(passwordHash);
            NickName = Ensure.String.IsNotNullOrEmpty(nickName);
            Email = Ensure.String.IsNotNullOrWhiteSpace(email);
        }

        public string PasswordHash { get; internal set; }
        public string NickName { get; internal set; }
        public string Email { get; internal set; }
    }
}