using ConsensusLibrary.Tools;

namespace ConsensusLibrary.UserContext
{
    public interface IRegistrationFacade
    {
        Identifier RegistrateUser(string email, string nickName, string password);
        bool CheckUserExistence(string email, string password);
    }
}
