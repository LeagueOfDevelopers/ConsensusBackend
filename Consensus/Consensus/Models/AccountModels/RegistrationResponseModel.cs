using System;
using System.ComponentModel.DataAnnotations;

namespace Consensus.Models.AccountModels
{
    /// <summary>
    ///     Модель, возвращаемая при регистрации пользователя
    /// </summary>
    public class RegistrationResponseModel
    {
        public RegistrationResponseModel(Guid id)
        {
            Id = id;
        }

        /// <summary>
        ///     Id нового пользователя
        /// </summary>
        [Required]
        public Guid Id { get; }
    }
}