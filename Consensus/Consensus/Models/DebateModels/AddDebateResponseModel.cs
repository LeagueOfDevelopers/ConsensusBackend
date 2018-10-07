using System;

namespace Consensus.Models.DebateModels
{
    /// <summary>
    /// Модель, возвращаемая при создании новых дебатов
    /// </summary>
    public class AddDebateResponseModel
    {
        /// <summary>
        /// Id созданных дебатов
        /// </summary>
        public Guid DebateId { get; }

        public AddDebateResponseModel(Guid debateId)
        {
            DebateId = debateId;
        }
    }
}
