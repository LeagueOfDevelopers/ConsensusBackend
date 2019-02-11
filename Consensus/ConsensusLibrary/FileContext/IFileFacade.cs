using ConsensusLibrary.FileContext.Views;
using ConsensusLibrary.Tools;
using Microsoft.AspNetCore.Http;

namespace ConsensusLibrary.FileContext
{
    public interface IFileFacade
    {
        string AddFile(Identifier userId, IFormFile file);
        GetFileView GetFile(Identifier userId, string fileName);
    }
}
