using System.Collections.Generic;

namespace Consensus.Models.ChatModels
{
    /// <summary>
    ///     Коллекция сообщений дебатов
    /// </summary>
    public class GetMessagesResponseModel
    {
        public GetMessagesResponseModel(IEnumerable<GetMessagesResponseItemModel> messages)
        {
            Messages = messages;
        }

        public IEnumerable<GetMessagesResponseItemModel> Messages { get; }
    }
}