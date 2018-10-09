namespace Consensus.Models.AccountModels
{
    /// <summary>
    ///     Ответ на успешный логин пользователя
    /// </summary>
    public class LoginResponseModel
    {
        public LoginResponseModel(string token)
        {
            Token = token;
        }

        /// <summary>
        ///     Bearer токен
        /// </summary>
        public string Token { get; }
    }
}