using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consensus.Models.VotingModels
{
    /// <summary>
    /// Модель получения результатов голосования
    /// </summary>
    public class GetVotingResultRequestModel
    {
        /// <summary>
        /// Id запрашиваемых дебатов
        /// </summary>
        public Guid DebateId { get; set; }
    }
}
