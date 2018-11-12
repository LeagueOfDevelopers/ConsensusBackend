using System;

namespace Consensus.Models.VotingModels
{
    /// <summary>
    ///     Модель получения результатов голосования
    /// </summary>
    public class GetVotingResultRequestModel
    {
        /// <summary>
        ///     Id запрашиваемых дебатов
        /// </summary>
        public Guid DebateId { get; set; }
    }
}