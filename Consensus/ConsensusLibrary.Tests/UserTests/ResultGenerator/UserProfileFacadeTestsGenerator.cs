using System.Collections.Generic;
using ConsensusLibrary.CryptoContext;
using ConsensusLibrary.FileContext;
using ConsensusLibrary.UserContext;

namespace ConsensusLibrary.Tests.UserTests.ResultGenerator
{
    public class UserProfileFacadeTestsGenerator
    {
        public static IEnumerable<object[]> GetConstructorParams()
        {
            yield return new object[]
            {
                new InMemoryUserRepository(),
                new InMemoryFileRepository(),
                null
            };

            yield return new object[]
            {
                new InMemoryUserRepository(),
                null,
                new CryptoServiceWithSalt(), 
            };

            yield return new object[]
            {
                null,
                new InMemoryFileRepository(),
                new CryptoServiceWithSalt(),
            };
        }
    }
}
