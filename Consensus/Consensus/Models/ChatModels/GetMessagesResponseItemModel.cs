using System;

namespace Consensus.Models.ChatModels
{
    /// <summary>
    /// Модель сообщения дебатов
    /// </summary>
    public class GetMessagesResponseItemModel
    {
        /// <summary>
        /// Id сообщения
        /// </summary>
        public Guid MessageId { get; }
        /// <summary>
        /// Id юзера
        /// </summary>
        public Guid UserId { get; }
        /// <summary>
        /// Время отправления
        /// </summary>
        public DateTimeOffset SentOn { get; }
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; }

        public GetMessagesResponseItemModel(
            Guid messageId, 
            Guid userId, 
            DateTimeOffset sentOn, 
            string text, 
            string userName)
        {
            MessageId = messageId;
            UserId = userId;
            SentOn = sentOn;
            Text = text;
            UserName = userName;
        }
    }
}
