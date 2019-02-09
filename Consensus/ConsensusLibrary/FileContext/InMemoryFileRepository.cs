using System.Collections.Generic;
using System.Linq;
using ConsensusLibrary.FileContext.Exceptions;
using EnsureThat;

namespace ConsensusLibrary.FileContext
{
    public class InMemoryFileRepository : IFileRepository
    {
        public InMemoryFileRepository()
        {
            _files = new List<File>();
        }

        private readonly List<File> _files;

        public void AddFile(File newFile)
        {
            Ensure.Any.IsNotNull(newFile);

            Ensure.Bool.IsFalse(_files.Any(f => f.FileName.Equals(newFile.FileName)),
                nameof(newFile), opt => opt.WithException(new FileAlreadyExistsException()));

            _files.Add(newFile);
        }

        public File GetFile(string fileName)
        {
            Ensure.String.IsNotNullOrWhiteSpace(fileName);

            var currentFile = _files.FirstOrDefault(f => f.FileName.Equals(fileName));

            Ensure.Any.IsNotNull(currentFile, nameof(currentFile),
                opt => opt.WithException(new FileNotFoundException()));

            return currentFile;
        }
    }
}
