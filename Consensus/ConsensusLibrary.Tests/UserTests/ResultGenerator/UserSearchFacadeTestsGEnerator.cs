using System.Collections.Generic;
using ConsensusLibrary.CategoryContext;
using ConsensusLibrary.DebateContext;
using ConsensusLibrary.UserContext;

namespace ConsensusLibrary.Tests.UserTests.ResultGenerator
{
    public class UserSearchFacadeTestsGenerator
    {
        public static IEnumerable<object[]> GetUserNames()
        {
            var userRepository = new InMemoryUserRepository();
            var debateRepository = new InMemoryDebateRepository();
            var categoryRepository = new InMemoryCategoryRepository();

            yield return new object[]
            {
                userRepository,
                debateRepository,
                categoryRepository,
                "123",
                0
            };

            userRepository = new InMemoryUserRepository();
            userRepository.AddUser(new User("123", "123", "123"));

            yield return new object[]
            {
                userRepository,
                debateRepository,
                categoryRepository,
                "123",
                1
            };

            userRepository = new InMemoryUserRepository();
            userRepository.AddUser(new User("123", "de1e", "123"));
            userRepository.AddUser(new User("123", "12aa3", "123"));
            userRepository.AddUser(new User("123", "123asde", "123"));

            yield return new object[]
            {
                userRepository,
                debateRepository,
                categoryRepository,
                "1",
                3
            };
        }

        public static IEnumerable<object[]> GetConstructorParams()
        {
            var userRepository = new InMemoryUserRepository();
            var debateRepository = new InMemoryDebateRepository();
            var categoryRepository = new InMemoryCategoryRepository();

            yield return new object[]
            {
                userRepository,
                debateRepository,
                null
            };

            yield return new object[]
            {
                userRepository,
                null,
                categoryRepository
            };

            yield return new object[]
            {
                null,
                debateRepository,
                categoryRepository
            };
        }
    }
}
