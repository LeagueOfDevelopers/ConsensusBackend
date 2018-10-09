using System.ComponentModel.DataAnnotations;

namespace Consensus.Models.PublishModels
{
    /// <summary>
    ///     Модель получения url для публикации
    /// </summary>
    public class GenerateTokenRequestModel
    {
        /// <summary>
        ///     Id сессии
        /// </summary>
        [Required]
        public string Session { get; set; }

        /// <summary>
        ///     Роль (SUBSCRIBER, PUBLISHER)
        /// </summary>
        [Required]
        public string Role { get; set; }
    }
}