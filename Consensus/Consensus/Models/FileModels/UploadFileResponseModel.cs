namespace Consensus.Models.FileModels
{
    public class UploadFileResponseModel
    {
        /// <summary>
        /// Индетификатор нового файла в системе
        /// </summary>
        public string FileName { get; }

        public UploadFileResponseModel(string fileName)
        {
            FileName = fileName;
        }
    }
}
