namespace ConsensusLibrary.FileContext.Views
{
    public class GetFileView
    {
        public string FileType { get; }
        public string PhysicalFileName { get; }

        public GetFileView(string fileType, string physicalFileName)
        {
            FileType = fileType;
            PhysicalFileName = physicalFileName;
        }
    }
}
