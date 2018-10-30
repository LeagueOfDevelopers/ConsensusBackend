using System;
using ConsensusLibrary.CryptoContext;
using ConsensusLibrary.Tests.UserTests.ResultGenerator;
using ConsensusLibrary.UserContext;
using ConsensusLibrary.UserContext.Exceptions;
using Xunit;

namespace ConsensusLibrary.Tests.UserTests
{
    public class RegistrationFacadeTests
    {
        [Theory]
        [MemberData(nameof(RegistrationFacadeTestsGenerator.GetConstructorParams),
            MemberType = typeof(RegistrationFacadeTestsGenerator))]
        public void Constructor_BadParams_Throws(IUserRepository userRepository, ICryptoService cryptoService)
        {
            Assert.Throws<ArgumentNullException>(() => new RegistrationFacade(userRepository, cryptoService));
        }

        [Theory]
        [MemberData(nameof(RegistrationFacadeTestsGenerator.GetRegisterUserParams),
            MemberType = typeof(RegistrationFacadeTestsGenerator))]
        public void RegisterUser_BadParams_Throws(string email, string nickName, string password)
        {
            //Arrange
            var userRepository = new InMemoryUserRepository();
            var cryptoService = new CryptoServiceWithSalt();
            var registrationFacade = new RegistrationFacade(userRepository, cryptoService);
            //Assert
            Assert.Throws<ArgumentNullException>(() => registrationFacade.RegisterUser(email, nickName, password));
        }

        [Fact]
        public void RegisterUser_ExistingUserName_Throws()
        {
            //Arrange
            var userRepository = new InMemoryUserRepository();
            var cryptoService = new CryptoServiceWithSalt();
            var registrationFacade = new RegistrationFacade(userRepository, cryptoService);
            var userName = "yaroslav";
            var password = "123123123";
            var email = "bus.yaroslav@gmail.com";
            //Act
            registrationFacade.RegisterUser(email, userName, password);
            //Assert
            Assert.Throws<UserAlreadyExistsException>(() => registrationFacade.RegisterUser("123@qwe.123", userName, password));
        }

        [Fact]
        public void RegisterUser_ExistingEmail_Throws()
        {
            //Arrange
            var userRepository = new InMemoryUserRepository();
            var cryptoService = new CryptoServiceWithSalt();
            var registrationFacade = new RegistrationFacade(userRepository, cryptoService);
            var userName = "yaroslav";
            var password = "123123123";
            var email = "bus.yaroslav@gmail.com";
            //Act
            registrationFacade.RegisterUser(email, userName, password);
            //Assert
            Assert.Throws<UserAlreadyExistsException>(() => registrationFacade.RegisterUser(email, "chokavo", password));
        }

        [Fact]
        public void RegisterUser_ExistingEmailAndUserName_Throws()
        {
            //Arrange
            var userRepository = new InMemoryUserRepository();
            var cryptoService = new CryptoServiceWithSalt();
            var registrationFacade = new RegistrationFacade(userRepository, cryptoService);
            var userName = "yaroslav";
            var password = "123123123";
            var email = "bus.yaroslav@gmail.com";
            //Act
            registrationFacade.RegisterUser(email, userName, password);
            //Assert
            Assert.Throws<UserAlreadyExistsException>(() => registrationFacade.RegisterUser(email, userName, password));
        }

        [Fact]
        public void CheckUserExistence_ExistingUser_ReturnView()
        {
            //Arrange
            var userRepository = new InMemoryUserRepository();
            var cryptoService = new CryptoServiceWithSalt();
            var registrationFacade = new RegistrationFacade(userRepository, cryptoService);
            var userName = "yaroslav";
            var password = "123123123";
            var email = "bus.yaroslav@gmail.com";
            registrationFacade.RegisterUser(email, userName, password);
            //Act
            var result = registrationFacade.CheckUserExistence(email, password);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void CheckUserExistence_NotExistingUser_ReturnNull()
        {
            //Arrange
            var userRepository = new InMemoryUserRepository();
            var cryptoService = new CryptoServiceWithSalt();
            var registrationFacade = new RegistrationFacade(userRepository, cryptoService);
            var password = "123123123";
            var email = "bus.yaroslav@gmail.com";
            //Act
            var result = registrationFacade.CheckUserExistence(email, password);
            //Assert
            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(RegistrationFacadeTestsGenerator.GetCheckUserExistenceNullParams),
            MemberType = typeof(RegistrationFacadeTestsGenerator))]
        public void CheckUserExistence_NullParams_Throws(string email, string password)
        {
            var userRepository = new InMemoryUserRepository();
            var cryptoService = new CryptoServiceWithSalt();
            var registrationFacade = new RegistrationFacade(userRepository, cryptoService);
            Assert.Throws<ArgumentNullException>(() => registrationFacade.CheckUserExistence(email, password));
        }
    }
}
