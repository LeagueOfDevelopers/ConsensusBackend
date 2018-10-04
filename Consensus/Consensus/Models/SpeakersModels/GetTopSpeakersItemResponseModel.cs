using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Consensus.Models.SpeakersModels
{
    /// <summary>
    /// Модель, возвращающая спикера
    /// </summary>
    public class GetTopSpeakersItemResponseModel
    {
        /// <summary>
        /// Никнейм пользователя
        /// </summary>
        [Required]
        public string NickName { get; }
        /// <summary>
        /// Репутация
        /// </summary>
        [Required]
        public int Reputation { get; }
        /// <summary>
        /// Аватарка
        /// </summary>
        [Required]
        public IFormFile Avatar { get; }

        public GetTopSpeakersItemResponseModel(
            string nickName,
            int reputation,
            IFormFile avatar)
        {
            NickName = nickName;
            Reputation = reputation;
            Avatar = avatar;
        }
    }
}
