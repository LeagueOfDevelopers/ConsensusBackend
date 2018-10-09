using System.Collections.Generic;
using EnsureThat;

namespace Consensus.Models.SpeakersModels
{
    /// <summary>
    ///     Модель, возвращающая список топа спикеров
    /// </summary>
    public class GetTopSpeakersResponseModel
    {
        public GetTopSpeakersResponseModel(IEnumerable<GetTopSpeakersItemResponseModel> speakers)
        {
            Speakers = Ensure.Any.IsNotNull(speakers);
        }

        /// <summary>
        ///     Список топа спикеров
        /// </summary>
        public IEnumerable<GetTopSpeakersItemResponseModel> Speakers { get; }
    }
}