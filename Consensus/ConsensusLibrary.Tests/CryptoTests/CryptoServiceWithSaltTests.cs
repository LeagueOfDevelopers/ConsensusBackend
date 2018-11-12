using ConsensusLibrary.CryptoContext;
using ConsensusLibrary.Tests.CryptoTests.ResultGenerator;
using Xunit;

namespace ConsensusLibrary.Tests.CryptoTests
{
    public class CryptoServiceWithSaltTests
    {
        [Theory]
        [MemberData(nameof(CryptoServiceWithSaltTestsGenerator.GetRightPasswords),
            MemberType = typeof(CryptoServiceWithSaltTestsGenerator))]
        public void CompareHashWithString_RightPasswords_True(string password, string expectedPassword)
        {
            //Arrange
            var cryptoService = new CryptoServiceWithSalt();
            var passwordHash = cryptoService.CalculateHash(password);
            //Act
            var result = cryptoService.CompareHashWithString(passwordHash, expectedPassword);
            //Assert
            Assert.True(result);
        }
    }
}