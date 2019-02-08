using System;
using System.Collections.Generic;
using System.Text;

namespace ConsensusLibrary.FileContext
{
    public interface IFileRepository
    {
        void AddFile(File newFile);
        File GetFile(string fileName);
    }
}
