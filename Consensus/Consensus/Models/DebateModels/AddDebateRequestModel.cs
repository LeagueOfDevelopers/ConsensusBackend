using System;
using System.ComponentModel.DataAnnotations;
using ConsensusLibrary.DebateContext;

namespace Consensus.Models.DebateModels
{
    /// <summary>
    ///     Модель добавления новых дебатов
    /// </summary>
    public class AddDebateRequestModel
    {
        /// <summary>
        ///     Время начала дебатов
        /// </summary>
        [Required]
        public DateTimeOffset StartDateTime { get; set; }

        /// <summary>
        ///     Название дебатов
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        ///     Id приглашающего юзера
        /// </summary>
        [Required]
        public Guid InviterOpponent { get; set; }

        /// <summary>
        ///     Id приглашенного юзера
        /// </summary>
        [Required]
        public Guid InvitedOpponent { get; set; }

        /// <summary>
        ///     Категория дебатов
        /// </summary>
        [Required]
        public DebateCategory DebateCategory { get; set; }
    }
}