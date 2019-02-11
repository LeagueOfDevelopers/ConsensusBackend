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

        public GetUserBySectionNameResponseItemModel(
            string userName,
            Identifier userIdentifier)
        {
            UserName = userName;
            UserIdentifier = userIdentifier;
        }
    }
}
