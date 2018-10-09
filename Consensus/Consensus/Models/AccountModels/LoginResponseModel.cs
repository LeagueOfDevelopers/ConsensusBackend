using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consensus.Models.AccountModels
{
    /// <summary>
    /// Ответ на успешный логин пользователя
    /// </summary>
    public class LoginResponseModel
    {
        /// <summary>
        /// Bearer токен
        /// </summary>
        public string Token { get; }

        public LoginResponseModel(string token)
        {
            Token = token;
        }
    }
}
