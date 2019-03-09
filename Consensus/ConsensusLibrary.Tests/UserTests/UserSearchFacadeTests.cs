using System;
using System.Linq;
using System.Text;
using ConsensusLibrary.CategoryContext;
using ConsensusLibrary.DebateContext;
using ConsensusLibrary.Tests.UserTests.ResultGenerator;
using ConsensusLibrary.UserContext;
using Xunit;

namespace ConsensusLibrary.Tests.UserTests
{
    public class UserSearchFacadeTests
    {
        const int pageSize = 5;
        const int pageNumber = 1;

        [Theory]
        [MemberData(nameof(UserSearchFacadeTestsGenerator.GetConstructorParams),
            MemberType = typeof(UserSearchFacadeTestsGenerator))]
        public void Constructor_BadParams_Throws(
            IUserRepository userRepository,
            IDebateRepository debateRepository,
            ICategoryRepository categoryRepository)
        {
            Assert.Throws<ArgumentNullException>(() => new UserSearchFacade(userRepository,
                debateRepository, categoryRepository));
        }

        [Fact]
        public void SearchUserByName_NullParam_Throws()
        {
            //Arrange
            var userRepository = new InMemoryUserRepository();
            var categoryRepository = new InMemoryCategoryRepository();
            var debateRepository = new InMemoryDebateRepository();
            var userSearchFacade = new UserSearchFacade(userRepository, debateRepository, categoryRepository);
            const string username = null;
            //Act
            Assert.Throws<ArgumentNullException>(() => userSearchFacade.SearchUserByName(username, pageSize, pageNumber));

        }

        [Fact]
        public void SearchUserByName_WhiteSpaceParam_Throws()
        {
            //Arrange
            var userRepository = new InMemoryUserRepository();
            var categoryRepository = new InMemoryCategoryRepository();
            var debateRepository = new InMemoryDebateRepository();
            var userSearchFacade = new UserSearchFacade(userRepository, debateRepository, categoryRepository);
            const string username = " ";
            //Act
            Assert.Throws<ArgumentException>(() => userSearchFacade.SearchUserByName(username, pageSize, pageNumber));

        }

        [Theory]
        [MemberData(nameof(UserSearchFacadeTestsGenerator.GetUserNames),
            MemberType = typeof(UserSearchFacadeTestsGenerator))]
        public void SearchUserByName_Username_GetResult(
            IUserRepository userRepository,
            IDebateRepository debateRepository,
            ICategoryRepository categoryRepository,
            string nameSector,
            int exceptedCount)
        {
            //Arrange
            var userSearchFacade = new UserSearchFacade(userRepository, debateRepository, categoryRepository);
            
            //Act
            var result = userSearchFacade.SearchUserByName(nameSector, pageSize, pageNumber).Users.Count();
            //Assert
            Assert.Equal(exceptedCount, result);
        }
    }
}
