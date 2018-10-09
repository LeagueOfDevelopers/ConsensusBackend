namespace Consensus.Models.PublishModels
{
    /// <summary>
    ///     Модель, содержащая url для публикации
    /// </summary>
    public class GenerateTokenResponseModel
    {
        /// <summary>
        ///     url для публикации
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///     То же, что было передано
        /// </summary>
        public string Session { get; set; }

        /// <summary>
        ///     То же, что было передано
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        ///     То же, что токен
        /// </summary>
        public string Id { get; set; }

        public string Data { get; set; }
    }
}