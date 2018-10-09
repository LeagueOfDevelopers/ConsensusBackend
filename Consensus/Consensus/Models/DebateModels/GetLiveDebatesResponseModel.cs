using System.Collections.Generic;

namespace Consensus.Models.DebateModels
{
    /// <summary>
    ///     Список дебатов, находящихся в эфире
    /// </summary>
    public class GetLiveDebatesResponseModel
    {
        public GetLiveDebatesResponseModel(
            IEnumerable<GetLiveDebatesResponseItemModel> debates)
        {
            Debates = debates;
        }

        /// <summary>
        ///     Список дебатов
        /// </summary>
        public IEnumerable<GetLiveDebatesResponseItemModel> Debates { get; }
    }
}