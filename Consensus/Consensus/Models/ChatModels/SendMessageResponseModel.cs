using System;

namespace Consensus.Models.ChatModels
{
    /// <summary>
    ///     Модель, являющая ответом на отправленное сообщение
    /// </summary>
    public class SendMessageResponseModel
    {
        public SendMessageResponseModel(Guid id)
        {
            Id = id;
        }

        /// <summary>
        ///     Id отправленного сообщения
        /// </summary>
        public Guid Id { get; }
    }
}