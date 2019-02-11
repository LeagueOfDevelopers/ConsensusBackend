using EnsureThat;

namespace ConsensusLibrary.FileContext
{
    public class File
    {
        public string FileName { get; }
        public string PhysicalFileName { get; }
        public string FileType { get; }

        public File(
            string fileName,
            string physicalFileName,
            string fileType)
        {
            FileName = Ensure.String.IsNotNullOrWhiteSpace(fileName);
            PhysicalFileName = Ensure.String.IsNotNullOrWhiteSpace(physicalFileName);
            FileType = Ensure.String.IsNotNullOrWhiteSpace(fileType);
        }
    }
}
