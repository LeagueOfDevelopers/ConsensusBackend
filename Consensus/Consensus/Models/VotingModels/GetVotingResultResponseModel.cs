using System;

namespace Consensus.Models.VotingModels
{
    public class GetVotingResultResponseModel
    {
        public GetVotingResultResponseModel(
            int firstDedaterVotesTotalCount,
            string firstDedaterNickName,
            Guid firstDedaterIdentifier,
            int secondDedaterVotesTotalCount,
            string secondDedaterNickName,
            Guid secondDedaterIdentifier)
        {
            FirstDedaterVotesTotalCount = firstDedaterVotesTotalCount;
            FirstDedaterNickName = firstDedaterNickName;
            FirstDedaterIdentifier = firstDedaterIdentifier;
            SecondDedaterVotesTotalCount = secondDedaterVotesTotalCount;
            SecondDedaterNickName = secondDedaterNickName;
            SecondDedaterIdentifier = secondDedaterIdentifier;
        }

        /// <summary>
        ///     Количество голосов за первого дебатера
        /// </summary>
        public int FirstDedaterVotesTotalCount { get; }

        public string FirstDedaterNickName { get; }
        public Guid FirstDedaterIdentifier { get; }

        /// <summary>
        ///     Количество голосов за второго дебатера
        /// </summary>
        public int SecondDedaterVotesTotalCount { get; }

        public string SecondDedaterNickName { get; }
        public Guid SecondDedaterIdentifier { get; }
    }
}