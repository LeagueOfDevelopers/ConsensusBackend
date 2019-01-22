using System;

namespace ConsensusLibrary.UserContext.Views
{
    public class LoginUserProfileView
    {
        public string NickName { get; }
        public string Email { get; }
        public DateTimeOffset RegistrationDateTime { get; }

        public LoginUserProfileView(
            string nickName,
            string email,
            DateTimeOffset registrationDateTime)
        {
            NickName = nickName;
            Email = email;
            RegistrationDateTime = registrationDateTime;
        }
    }
}