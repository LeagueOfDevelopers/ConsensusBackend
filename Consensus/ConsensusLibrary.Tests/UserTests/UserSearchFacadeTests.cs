using System;
using System.Linq;
using ConsensusLibrary.Tests.UserTests.ResultGenerator;
using ConsensusLibrary.UserContext;
using Xunit;

namespace ConsensusLibrary.Tests.UserTests
{
    public class UserSearchFacadeTests
    {
        [Fact]
        public void Constructor_BadParams_Throws()
        {
            //Arrange
            const IUserRepository userRepository = null;
            //Act
            Assert.Throws<ArgumentNullException>(() => new UserSearchFacade(userRepository));
        }

        [Fact]
        public void SearchUserByName_NullParam_Throws()
        {
            //Arrange
            var userRepository = new InMemoryUserRepository();
            var userSearchFacade = new UserSearchFacade(userRepository);
            const string username = null;
            //Act
            Assert.Throws<ArgumentNullException>(() => userSearchFacade.SearchUserByName(username, 1, 1));
        }

        [Fact]
        public void SearchUserByName_WhiteSpaceParam_Throws()
        {
            //Arrange
            var userRepository = new InMemoryUserRepository();
            var userSearchFacade = new UserSearchFacade(userRepository);
            const string username = " ";
            //Act
            Assert.Throws<ArgumentException>(() => userSearchFacade.SearchUserByName(username, 1, 1));
        }

        [Theory]
        [MemberData(nameof(UserSearchFacadeTestsGenerator.GetUserNames),
            MemberType = typeof(UserSearchFacadeTestsGenerator))]
        public void SearchUserByName_Username_GetResult(
            IUserRepository userRepository,
            string nameSector,
            int exceptedCount)
        {
            //Arrange
            var userSearchFacade = new UserSearchFacade(userRepository);
            //Act
            var result = userSearchFacade.SearchUserByName(nameSector, 5, 1).Users.Count();
            //Assert
            Assert.Equal(exceptedCount, result);
        }
    }
}
