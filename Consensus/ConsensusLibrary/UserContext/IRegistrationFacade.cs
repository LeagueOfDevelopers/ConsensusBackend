using ConsensusLibrary.Tools;
using ConsensusLibrary.UserContext.Views;

namespace ConsensusLibrary.UserContext
{
    public interface IRegistrationFacade
    {
        Identifier RegisterUser(string email, string nickName, string password);
        CheckUserExistenceView CheckUserExistence(string email, string password);
    }
}