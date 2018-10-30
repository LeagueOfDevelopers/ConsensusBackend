using System;

namespace Consensus.Models.ChatModels
{
    /// <summary>
    /// Модель, являющая ответом на отправленное сообщение
    /// </summary>
    public class SendMessageResponseModel
    {
        /// <summary>
        /// Id отправленного сообщения
        /// </summary>
        public Guid Id { get; }

        public SendMessageResponseModel(Guid id)
        {
            Id = id;
        }
    }
}
