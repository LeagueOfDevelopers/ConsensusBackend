using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Consensus.Models.SpeakersModels
{
    /// <summary>
    ///     Модель, возвращающая спикера
    /// </summary>
    public class GetTopSpeakersItemResponseModel
    {
        public GetTopSpeakersItemResponseModel(
            string nickName,
            int reputation,
            IFormFile avatar)
        {
            NickName = nickName;
            Reputation = reputation;
            Avatar = avatar;
        }

        /// <summary>
        ///     Никнейм пользователя
        /// </summary>
        [Required]
        public string NickName { get; }

        /// <summary>
        ///     Репутация
        /// </summary>
        [Required]
        public int Reputation { get; }

        /// <summary>
        ///     Аватарка
        /// </summary>
        [Required]
        public IFormFile Avatar { get; }
    }
}