using System.Collections.Generic;

namespace ConsensusLibrary.Tests.CryptoTests.ResultGenerator
{
    public class CryptoServiceWithSaltTestsGenerator
    {
        public static IEnumerable<object[]> GetRightPasswords()
        {
            yield return new object[]
            {
                "123",
                "123"
            };
            yield return new object[]
            {
                "!@&#!()@#!NDOQBDOAS",
                "!@&#!()@#!NDOQBDOAS"
            };
            yield return new object[]
            {
                string.Empty,
                string.Empty
            };
        }
    }
}