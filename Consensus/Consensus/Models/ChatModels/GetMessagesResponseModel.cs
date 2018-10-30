using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consensus.Models.ChatModels
{
    /// <summary>
    /// Коллекция сообщений дебатов
    /// </summary>
    public class GetMessagesResponseModel
    {
        public IEnumerable<GetMessagesResponseItemModel> Messages { get; }

        public GetMessagesResponseModel(IEnumerable<GetMessagesResponseItemModel> messages)
        {
            Messages = messages;
        }
    }
}
