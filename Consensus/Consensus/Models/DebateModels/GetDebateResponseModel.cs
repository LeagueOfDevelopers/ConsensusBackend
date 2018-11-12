using System;
using System.Collections.Generic;
using ConsensusLibrary.DebateContext;

namespace Consensus.Models.DebateModels
{
    public class GetDebateResponseModel
    {
        public GetDebateResponseModel(
            DateTimeOffset startDateTime, 
            string title, 
            DebateCategory category, 
            DebateState state,
            IEnumerable<DebateMemberResponseModel> members)
        {
            StartDateTime = startDateTime;
            Title = title;
            Category = category;
            State = state;
            Members = members;
        }
        
        /// <summary>
        ///     Время начала дебатов
        /// </summary>
        public DateTimeOffset StartDateTime { get; }

        /// <summary>
        ///     Название дебатов
        /// </summary>
        public string Title { get; }

        /// <summary>
        ///     Категория дебатов
        /// </summary>
        public DebateCategory Category { get; }
        /// <summary>
        /// Состояние дебатов
        /// </summary>
        public DebateState State { get; }
        public IEnumerable<DebateMemberResponseModel> Members { get; }
    }
}