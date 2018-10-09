using System;

namespace Consensus.Models.DebateModels
{
    /// <summary>
    ///     Модель, возвращаемая при создании новых дебатов
    /// </summary>
    public class AddDebateResponseModel
    {
        public AddDebateResponseModel(Guid debateId)
        {
            DebateId = debateId;
        }

        /// <summary>
        ///     Id созданных дебатов
        /// </summary>
        public Guid DebateId { get; }
    }
}