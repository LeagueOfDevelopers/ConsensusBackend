using EnsureThat;

namespace ConsensusLibrary.FileContext
{
    public class FileSettings
    {
        public string UploadPath { get; }

        public FileSettings(string uploadPath)
        {
            UploadPath = Ensure.String.IsNotNullOrWhiteSpace(uploadPath);
        }
    }
}
