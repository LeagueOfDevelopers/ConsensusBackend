using System.ComponentModel.DataAnnotations;
using ConsensusLibrary.DebateContext;
using Microsoft.AspNetCore.Http;

namespace Consensus.Models.DebateModels
{
    /// <summary>
    ///     Модель прошедших дебатов
    /// </summary>
    public class GetPastDebatesResponseItemModel
    {
        public GetPastDebatesResponseItemModel(
            int id,
            string title,
            int firstDebaterId,
            int secondDebaterId,
            string firstDebaterName,
            string secondDebaterName,
            string spectatorsCount,
            string theme,
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
        public int Id { get; }

        /// <summary>
        ///     Заголовок дебата
        /// </summary>
        [Required]
        public string Title { get; }

        /// <summary>
        ///     Уникальный идентификатор первого участника дебатов
        /// </summary>
        [Required]
        public int FirstDebaterId { get; }

        /// <summary>
        ///     Уникальный идентификатор второго участника дебатов
        /// </summary>
        [Required]
        public int SecondDebaterId { get; }

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
        public string SpectatorsCount { get; }

        /// <summary>
        ///     Тема дебатов
        /// </summary>
        [Required]
        public string Theme { get; }

        /// <summary>
        ///     Миниатюра дебатов
        /// </summary>
        [Required]
        public IFormFile Thumbnail { get; }
    }
}