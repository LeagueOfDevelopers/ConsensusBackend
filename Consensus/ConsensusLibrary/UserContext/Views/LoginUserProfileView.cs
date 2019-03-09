using System;

namespace ConsensusLibrary.UserContext.Views
{
    public class LoginUserProfileView
    {
        public string NickName { get; }
        public string Email { get; }
        public DateTimeOffset RegistrationDateTime { get; }
        public string Avatar { get; }

        public LoginUserProfileView(
            string nickName, 
            string email, 
            DateTimeOffset registrationDateTime, 
            string avatar)
        {
            NickName = nickName;
            Email = email;
            RegistrationDateTime = registrationDateTime;
            Avatar = avatar;
        }
    }
}