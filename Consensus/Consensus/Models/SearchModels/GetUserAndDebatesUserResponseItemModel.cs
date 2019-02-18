using System;

namespace Consensus.Models.SearchModels
{
    public class GetUserAndDebatesUserResponseItemModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string NickName { get; }
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid UserIdentifier { get; }
        /// <summary>
        /// Имя файла
        /// </summary>
        public string UserAvatarFileName { get; }
        /// <summary>
        /// Количество побед
        /// </summary>
        public int WinCount { get; }
        /// <summary>
        /// Количество поражений
        /// </summary>
        public int LossCount { get; }

        public GetUserAndDebatesUserResponseItemModel(
            string nickName,
            Guid userIdentifier, 
            string userAvatarFileName, 
            int winCount, 
            int lossCount)
        {
            NickName = nickName;
            UserIdentifier = userIdentifier;
            UserAvatarFileName = userAvatarFileName;
            WinCount = winCount;
            LossCount = lossCount;
        }
    }
}
