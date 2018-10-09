namespace ConsensusLibrary.CryptoContext
{
    public interface ICryptoService
    {
        string CalculateHash(string password);
        bool CompareHashWithString(string hash, string password);
    }
}