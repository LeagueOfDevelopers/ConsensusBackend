using System;
using System.Collections.Generic;
using System.Text;
using ConsensusLibrary.CryptoContext;
using ConsensusLibrary.UserContext;

namespace ConsensusLibrary.Tests.UserTests.ResultGenerator
{
    public class RegistrationFacadeTestsGenerator
    {
        public static IEnumerable<object[]> GetConstructorParams()
        {
            yield return new object[]
            {
                null,
                null
            };
            yield return new object[]
            {
                null,
                new CryptoServiceWithSalt()
            };
            yield return new object[]
            {
                new InMemoryUserRepository(),
                null
            };
        }

        public static IEnumerable<object[]> GetRegisterUserParams()
        {
            yield return new object[]
            {
                null,
                string.Empty,
                string.Empty
            };
            yield return new object[]
            {
                string.Empty,
                null,
                string.Empty
            };
            yield return new object[]
            {
                string.Empty,
                string.Empty,
                null
            };
        }

        public static IEnumerable<object[]> GetCheckUserExistenceNullParams()
        {
            yield return new object[]
            {
                null,
                string.Empty,
            };
            yield return new object[]
            {
                string.Empty,
                string.Empty
            };
            yield return new object[]
            {
                string.Empty,
                null
            };
        }
    }
}
