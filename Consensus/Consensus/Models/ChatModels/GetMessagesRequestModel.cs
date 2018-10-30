using System;
using System.ComponentModel.DataAnnotations;

namespace Consensus.Models.ChatModels
{
    /// <summary>
    /// Модель запроса сообщений из чата дебатов
    /// </summary>
    public class GetMessagesRequestModel
    {
        /// <summary>
        /// Id дебатов, из которых получаем сообщения
        /// </summary>
        [Required]
        public Guid DebateId { get; set; }
    }
}
