using EnsureThat;
using System.Collections.Generic;

namespace Consensus.Models.SpeakersModels
{
    /// <summary>
    /// Модель, возвращающая список топа спикеров
    /// </summary>
    public class GetTopSpeakersResponseModel
    {
        /// <summary>
        /// Список топа спикеров
        /// </summary>
        public IEnumerable<GetTopSpeakersItemResponseModel> Speakers { get; }

        public GetTopSpeakersResponseModel(IEnumerable<GetTopSpeakersItemResponseModel> speakers)
        {
            Speakers = Ensure.Any.IsNotNull(speakers);
        }
    }
}
