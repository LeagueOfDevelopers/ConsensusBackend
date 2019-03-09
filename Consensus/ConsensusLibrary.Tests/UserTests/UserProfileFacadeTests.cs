using System;
using ConsensusLibrary.CryptoContext;
using ConsensusLibrary.FileContext;
using ConsensusLibrary.FileContext.Exceptions;
using ConsensusLibrary.Tests.UserTests.ResultGenerator;
using ConsensusLibrary.UserContext;
using ConsensusLibrary.UserContext.Exceptions;
using Xunit;

namespace ConsensusLibrary.Tests.UserTests
{
    public class UserProfileFacadeTests
    {
        [Theory]
        [MemberData(nameof(UserProfileFacadeTestsGenerator.GetConstructorParams),
            MemberType = typeof(UserProfileFacadeTestsGenerator))]
        public void Constructor_BadParams_Throws(
            IUserRepository userRepository,
            IFileRepository fileRepository,
            ICryptoService cryptoService)
        {
            Assert.Throws<ArgumentNullException>(() =>
                new UserProfileFacade(userRepository, fileRepository, cryptoService));
        }

        [Fact]
        public void EditName_NameExists_Throws()
        {
            //Arrange
            var userRepository = new InMemoryUserRepository();
            var fileRepository = new InMemoryFileRepository();
            var cryptoService = new CryptoServiceWithSalt();

            userRepository.AddUser(new User("email", "nick", "123"));
            var targetUser = new User("email1", "nick1", "123");
            userRepository.AddUser(targetUser);

            var userProfileFacade = new UserProfileFacade(userRepository,
                fileRepository, cryptoService);

            const string newName = "nick";

            //Act
            Assert.Throws<UserAlreadyExistsException>(() =>
                userProfileFacade.ChangeUserName(targetUser.Identifier, newName));
        }

        [Fact]
        public void EditEmail_EmailExists_Throws()
        {
            //Arrange
            var userRepository = new InMemoryUserRepository();
            var fileRepository = new InMemoryFileRepository();
            var cryptoService = new CryptoServiceWithSalt();

            userRepository.AddUser(new User("email", "nick", "123"));
            var targetUser = new User("email1", "nick1", "123");
            userRepository.AddUser(targetUser);

            var userProfileFacade = new UserProfileFacade(userRepository,
                fileRepository, cryptoService);

            const string newEmail = "email";

            //Act
            Assert.Throws<UserAlreadyExistsException>(() =>
                userProfileFacade.ChangeUserEmail(targetUser.Identifier, newEmail));
        }

        [Fact]
        public void EditEmail_EmailDoesNotExist_EmailChanged()
        {
            //Arrange
            var userRepository = new InMemoryUserRepository();
            var fileRepository = new InMemoryFileRepository();
            var cryptoService = new CryptoServiceWithSalt();
            var userProfileFacade = new UserProfileFacade(userRepository,
                fileRepository, cryptoService);

            var targetUser = new User("email1", "nick1", "123");
            userRepository.AddUser(targetUser);

            const string newEmail = "email";
            //Act
            userProfileFacade.ChangeUserEmail(targetUser.Identifier, newEmail);
            var changedUser = userRepository.GetUserById(targetUser.Identifier);
            //Assert
            Assert.Equal(newEmail, changedUser.Credentials.Email);
        }

        [Fact]
        public void EditName_NameDoesNotExist_NameChanged()
        {
            //Arrange
            var userRepository = new InMemoryUserRepository();
            var fileRepository = new InMemoryFileRepository();
            var cryptoService = new CryptoServiceWithSalt();
            var userProfileFacade = new UserProfileFacade(userRepository,
                fileRepository, cryptoService);

            var targetUser = new User("email1", "nick1", "123");
            userRepository.AddUser(targetUser);

            const string newName = "nick";
            //Act
            userProfileFacade.ChangeUserName(targetUser.Identifier, newName);
            var changedUser = userRepository.GetUserById(targetUser.Identifier);
            //Assert
            Assert.Equal(newName, changedUser.Credentials.NickName);
        }

        [Fact]
        public void EditAbout_AboutIsNotEmpty_AboutChanged()
        {
            //Arrange
            var userRepository = new InMemoryUserRepository();
            var fileRepository = new InMemoryFileRepository();
            var cryptoService = new CryptoServiceWithSalt();
            var userProfileFacade = new UserProfileFacade(userRepository,
                fileRepository, cryptoService);

            var targetUser = new User("email1", "nick1", "123");
            userRepository.AddUser(targetUser);

            const string newAbout = "about 123";
            //Act
            userProfileFacade.ChangeUserAbout(targetUser.Identifier, newAbout);
            var changedUser = userRepository.GetUserById(targetUser.Identifier);
            //Assert
            Assert.Equal(newAbout, changedUser.UserProfile.About);
        }

        [Fact]
        public void EditPassword_PasswordIsNotEmpty_PasswordChanged()
        {
            //Arrange
            var userRepository = new InMemoryUserRepository();
            var fileRepository = new InMemoryFileRepository();
            var cryptoService = new CryptoServiceWithSalt();
            var userProfileFacade = new UserProfileFacade(userRepository,
                fileRepository, cryptoService);

            var targetUser = new User("email1", "nick1", "123");
            userRepository.AddUser(targetUser);

            const string newPassword = "newpassword123";

            //Act
            userProfileFacade.ChangeUserPassword(targetUser.Identifier, newPassword);
            var changedUser = userRepository.GetUserById(targetUser.Identifier);

            var result = cryptoService.CompareHashWithString(
                changedUser.Credentials.PasswordHash, newPassword);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void EditAvatar_AvatarFileExists_AvatarChanged()
        {
            //Arrange
            var userRepository = new InMemoryUserRepository();
            var fileRepository = new InMemoryFileRepository();
            var cryptoService = new CryptoServiceWithSalt();
            var userProfileFacade = new UserProfileFacade(userRepository,
                fileRepository, cryptoService);

            const string fileName = "filename";

            fileRepository.AddFile(new File(fileName, "physicalFilename", "type"));

            var targetUser = new User("email1", "nick1", "123");
            userRepository.AddUser(targetUser);

            //Act
            userProfileFacade.ChangeUserAvatar(targetUser.Identifier, fileName);
            var changedUser = userRepository.GetUserById(targetUser.Identifier);

            //Assert
            Assert.Equal(fileName, changedUser.UserProfile.AvatarLink);
        }

        [Fact]
        public void EditAvatar_AvatarFileDesNotExist_Throws()
        {
            //Arrange
            var userRepository = new InMemoryUserRepository();
            var fileRepository = new InMemoryFileRepository();
            var cryptoService = new CryptoServiceWithSalt();
            var userProfileFacade = new UserProfileFacade(userRepository,
                fileRepository, cryptoService);

            const string fileName = "filename";

            var targetUser = new User("email1", "nick1", "123");
            userRepository.AddUser(targetUser);

            //Act
            Assert.Throws<FileNotFoundException>(() =>
                userProfileFacade.ChangeUserAvatar(targetUser.Identifier, fileName));
        }
    }
}
