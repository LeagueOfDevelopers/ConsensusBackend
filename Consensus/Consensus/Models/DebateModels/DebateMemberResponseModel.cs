using System;

namespace Consensus.Models.DebateModels
{
    public class DebateMemberResponseModel
    {
        public DebateMemberResponseModel(
            string nickName,
            Guid identifier,
            bool ready,
            string translationLink)
        {
            NickName = nickName;
            Identifier = identifier;
            Ready = ready;
            TranslationLink = translationLink;
        }


        /// <summary>
        ///     NickName дебатера
        /// </summary>
        public string NickName { get; }

        /// <summary>
        ///     Id дебатера
        /// </summary>
        public Guid Identifier { get; }

        /// <summary>
        ///     Готовность начать сражение
        /// </summary>
        public bool Ready { get; }

        /// <summary>
        ///     Ссылка для трансляции
        /// </summary>
        public string TranslationLink { get; }
    }
}