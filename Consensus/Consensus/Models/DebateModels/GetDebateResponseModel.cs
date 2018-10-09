using System;
using ConsensusLibrary.DebateContext;

namespace Consensus.Models.DebateModels
{
    public class GetDebateResponseModel
    {
        public GetDebateResponseModel(
            Guid identifier,
            string leftFighterNickName,
            Guid leftFighterId,
            string rightFighterNickName,
            Guid rightFighterId,
            DateTimeOffset startDateTime,
            DateTimeOffset endDateTime,
            int viewerCount,
            string title,
            DebateCategory category)
        {
            Identifier = identifier;
            LeftFighterNickName = leftFighterNickName;
            LeftFighterId = leftFighterId;
            RightFighterNickName = rightFighterNickName;
            RightFighterId = rightFighterId;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            ViewerCount = viewerCount;
            Title = title;
            Category = category;
        }

        /// <summary>
        ///     Id дебатов
        /// </summary>
        public Guid Identifier { get; }

        /// <summary>
        ///     NickName первого оппонента
        /// </summary>
        public string LeftFighterNickName { get; }

        /// <summary>
        ///     Id первого оппонента
        /// </summary>
        public Guid LeftFighterId { get; }

        /// <summary>
        ///     NickName второго оппонента
        /// </summary>
        public string RightFighterNickName { get; }

        /// <summary>
        ///     Id второго оппонента
        /// </summary>
        public Guid RightFighterId { get; }

        /// <summary>
        ///     Время начала дебатов
        /// </summary>
        public DateTimeOffset StartDateTime { get; }

        /// <summary>
        ///     Время окончания дебатов
        /// </summary>
        public DateTimeOffset EndDateTime { get; }

        /// <summary>
        ///     Количество зрителей в данный момент
        /// </summary>
        public int ViewerCount { get; }

        /// <summary>
        ///     Название дебатов
        /// </summary>
        public string Title { get; }

        /// <summary>
        ///     Категория дебатов
        /// </summary>
        public DebateCategory Category { get; }
    }
}