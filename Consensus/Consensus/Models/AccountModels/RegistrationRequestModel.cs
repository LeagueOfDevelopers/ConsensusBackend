using System.ComponentModel.DataAnnotations;

namespace Consensus.Models.AccountModels
{
    /// <summary>
    /// Модель регистрации пользователя
    /// </summary>
    public class RegistrationRequestModel
    {
        [Required]
        public string NickName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
