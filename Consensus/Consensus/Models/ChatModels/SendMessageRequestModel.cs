using System;
using System.ComponentModel.DataAnnotations;

namespace Consensus.Models.ChatModels
{
    /// <summary>
    ///     Модель отправки сообщения в чат
    /// </summary>
    public class SendMessageRequestModel
    {
        /// <summary>
        ///     Текст сообщения
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        ///     Id дебатов, в чат которых отправляем
        /// </summary>
        [Required]
        public Guid debateId { get; set; }
    }
}