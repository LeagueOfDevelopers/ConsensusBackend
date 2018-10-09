using System.ComponentModel.DataAnnotations;

namespace Consensus.Models.AccountModels
{
    /// <summary>
    ///     Модель логина пользователя
    /// </summary>
    public class LoginRequestModel
    {
        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }
    }
}