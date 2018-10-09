using System.Collections.Generic;

namespace Consensus.Models.DebateModels
{
    /// <summary>
    ///     Модель, возращающая список прошедших дебатов
    /// </summary>
    public class GetPastDebatesResponseModel
    {
        public GetPastDebatesResponseModel(IEnumerable<GetPastDebatesResponseItemModel> debates)
        {
            Debates = debates;
        }

        /// <summary>
        ///     Список прошедших дебатов
        /// </summary>
        public IEnumerable<GetPastDebatesResponseItemModel> Debates { get; }
    }
}