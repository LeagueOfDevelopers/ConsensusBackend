using System;
using System.ComponentModel.DataAnnotations;
using ConsensusLibrary.DebateContext;
using Microsoft.AspNetCore.Http;

namespace Consensus.Models.DebateModels
{
    /// <summary>
    ///     Модель дебатов, находящихся в эфире
    /// </summary>
    public class GetLiveDebatesResponseItemModel
    {
        public GetLiveDebatesResponseItemModel(
            Guid id,
            string title,
            Guid firstDebaterId,
            Guid secondDebaterId,
            string firstDebaterName,
            string secondDebaterName,
            int spectatorsCount,
            DebateCategory theme,
            IFormFile thumbnail)
        {
            Id = id;
            Title = title;
            FirstDebaterId = firstDebaterId;
            SecondDebaterId = secondDebaterId;
            FirstDebaterName = firstDebaterName;
            SecondDebaterName = secondDebaterName;
            SpectatorsCount = spectatorsCount;
            Theme = theme;
            Thumbnail = thumbnail;
        }

        /// <summary>
        ///     Уникальный идентификатор
        /// </summary>
        [Required]
        public Guid Id { get; }

        /// <summary>
        ///     Заголовок дебата
        /// </summary>
        [Required]
        public string Title { get; }

        /// <summary>
        ///     Уникальный идентификатор первого участника дебатов
        /// </summary>
        [Required]
        public Guid FirstDebaterId { get; }

        /// <summary>
        ///     Уникальный идентификатор второго участника дебатов
        /// </summary>
        [Required]
        public Guid SecondDebaterId { get; }

        /// <summary>
        ///     Имя первого участника дебатов
        /// </summary>
        [Required]
        public string FirstDebaterName { get; }

        /// <summary>
        ///     Имя второго участника дебатов
        /// </summary>
        [Required]
        public string SecondDebaterName { get; }

        /// <summary>
        ///     Количество зрителей
        /// </summary>
        [Required]
        public int SpectatorsCount { get; }

        /// <summary>
        ///     Тема дебатов
        /// </summary>
        [Required]
        public DebateCategory Theme { get; }

        /// <summary>
        ///     Миниатюра дебатов
        /// </summary>
        [Required]
        public IFormFile Thumbnail { get; }
    }
}