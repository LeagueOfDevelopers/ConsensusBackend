using System;
using System.IO;
using ConsensusLibrary.FileContext.Views;
using ConsensusLibrary.Tools;
using ConsensusLibrary.UserContext;
using EnsureThat;
using Microsoft.AspNetCore.Http;

namespace ConsensusLibrary.FileContext
{
    public class FileFacade : IFileFacade
    {
        public string AddFile(Identifier userId, IFormFile file)
        {
            Ensure.Any.IsNotNull(userId);
            Ensure.Any.IsNotNull(file);
            var currentUser = _userRepository.GetUserById(userId);

            var directoryForDate = Path.Combine(_fileSettings.UploadPath,
                DateTime.UtcNow.ToShortDateString());

            if (!Directory.Exists(directoryForDate))
                Directory.CreateDirectory(directoryForDate);

            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(directoryForDate, newFileName);

            var newFile = new File(newFileName, filePath, file.ContentType);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            _fileRepository.AddFile(newFile);

            return newFileName;
        }

        public GetFileView GetFile(Identifier userId, string fileName)
        {
            Ensure.Any.IsNotNull(userId);
            Ensure.String.IsNotNullOrWhiteSpace(fileName);

            var currentUser = _userRepository.GetUserById(userId);

            var currentFile = _fileRepository.GetFile(fileName);

            var result = new GetFileView(currentFile.FileType, currentFile.PhysicalFileName);

            return result;
        }

        public FileFacade(
            IFileRepository fileRepository,
            IUserRepository userRepository,
            FileSettings fileSettings)
        {
            _fileRepository = Ensure.Any.IsNotNull(fileRepository);
            _userRepository = Ensure.Any.IsNotNull(userRepository);
            _fileSettings = Ensure.Any.IsNotNull(fileSettings);
        }

        private readonly IFileRepository _fileRepository;
        private readonly IUserRepository _userRepository;
        private readonly FileSettings _fileSettings;
    }
}
