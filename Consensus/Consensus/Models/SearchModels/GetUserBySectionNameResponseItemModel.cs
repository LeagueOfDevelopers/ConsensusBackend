using ConsensusLibrary.Tools;

namespace Consensus.Models.SearchModels
{
    public class GetUserBySectionNameResponseItemModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; }
        /// <summary>
        /// Объект, содержащий id пользователя
        /// </summary>
        public Identifier UserIdentifier { get; }
        /// <summary>
        /// Уникальный индентификатор аватарки пользователя в системе
        /// </summary>
        public string Avatar { get; }

        public GetUserBySectionNameResponseItemModel(
            string userName,
            Identifier userIdentifier,
            string avatar)
        {
            UserName = userName;
            UserIdentifier = userIdentifier;
            Avatar = avatar;
        }
    }
}
