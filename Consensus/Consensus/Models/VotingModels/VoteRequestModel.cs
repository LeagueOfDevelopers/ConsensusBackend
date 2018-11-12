using System;

namespace Consensus.Models.VotingModels
{
    /// <summary>
    ///     Модель отправление голоса
    /// </summary>
    public class VoteRequestModel
    {
        /// <summary>
        ///     Id дебатера, которому отдается голос
        /// </summary>
        public Guid ToUser { get; set; }

        /// <summary>
        ///     Id дебатов
        /// </summary>
        public Guid DebateId { get; set; }
    }
}