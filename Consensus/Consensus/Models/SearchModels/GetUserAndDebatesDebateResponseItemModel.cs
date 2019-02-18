using System;

namespace Consensus.Models.SearchModels
{
    public class GetUserAndDebatesDebateResponseItemModel
    {
        /// <summary>
        /// Название дебатов
        /// </summary>
        public string DebateTitle { get; }
        /// <summary>
        /// Id дебатов
        /// </summary>
        public Guid DebateIdentifier { get; }
        /// <summary>
        /// Статус дебатов
        /// </summary>
        public bool IsLive { get; }
        /// <summary>
        /// Категория дебатов
        /// </summary>
        public string Category { get; }
        /// <summary>
        /// Имя пригласившего
        /// </summary>
        public string InviterNickName { get; }
        /// <summary>
        /// Id пригласившего
        /// </summary>
        public Guid InviterIdentifier { get; }
        /// <summary>
        /// Имя приглашенного
        /// </summary>
        public string InvitedNickName { get; }
        /// <summary>
        /// Id приглашенного
        /// </summary>
        public Guid InvitedIdentifier { get; }

        public GetUserAndDebatesDebateResponseItemModel(
            string debateTitle,
            Guid debateIdentifier,
            bool isLive,
            string category,
            string inviterNickName,
            Guid inviterIdentifier,
            string invitedNickName,
            Guid invitedIdentifier)
        {
            DebateTitle = debateTitle;
            DebateIdentifier = debateIdentifier;
            IsLive = isLive;
            Category = category;
            InviterNickName = inviterNickName;
            InviterIdentifier = inviterIdentifier;
            InvitedNickName = invitedNickName;
            InvitedIdentifier = invitedIdentifier;
        }
    }
}
