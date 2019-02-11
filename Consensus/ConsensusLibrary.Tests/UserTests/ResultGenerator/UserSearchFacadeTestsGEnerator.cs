using System.Collections.Generic;
using ConsensusLibrary.UserContext;

namespace ConsensusLibrary.Tests.UserTests.ResultGenerator
{
    public class UserSearchFacadeTestsGenerator
    {
        public static IEnumerable<object[]> GetUserNames()
        {
            var userRepository = new InMemoryUserRepository();

            yield return new object[]
            {
                userRepository,
                "123",
                0
            };

            userRepository = new InMemoryUserRepository();
            userRepository.AddUser(new User("123", "123", "123"));

            yield return new object[]
            {
                userRepository,
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
                "1",
                3
            };
        }
    }
}
