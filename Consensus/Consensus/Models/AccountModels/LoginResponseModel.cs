using System;

namespace Consensus.Models.AccountModels
{
    /// <summary>
    ///     Ответ на успешный логин пользователя
    /// </summary>
    public class LoginResponseModel
    {
        public LoginResponseModel(
            string token, 
            string nickName, 
            string email, 
            DateTimeOffset registrationDateTime, 
            string avatar)
        {
            Token = token;
            NickName = nickName;
            Email = email;
            RegistrationDateTime = registrationDateTime;
            Avatar = avatar;
        }



        /// <summary>
        ///     Bearer токен
        /// </summary>
        public string Token { get; }
        public string NickName { get; }
        public string Email { get; }
        /// <summary>
        ///     Дата регистрации пользователя
        /// </summary>
        public DateTimeOffset RegistrationDateTime { get; }
        /// <summary>
        /// Уникальный идентификатор файла в системе
        /// </summary>
        public string Avatar { get; }
    }
}