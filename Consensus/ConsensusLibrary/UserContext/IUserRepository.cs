using ConsensusLibrary.Tools;

namespace ConsensusLibrary.UserContext
{
    public interface IUserRepository
    {
        void AddUser(User user);
        void UpdateUser(User user);
        User GetUserById(Identifier identifier);
        User GetUserByCredentials(Credentials credentials);
        User TryGetUserByEmail(string email);
        User TryGetUserByEmailOrNickName(string email, string nickName);
    }
}
