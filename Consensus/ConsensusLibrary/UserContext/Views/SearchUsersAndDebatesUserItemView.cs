using System;

namespace ConsensusLibrary.UserContext.Views
{
    public class SearchUsersAndDebatesUserItemView
    {
        public string NickName { get; }
        public Guid UserIdentifier { get; }
        public string UserAvatarFileName { get; }
        public int WinCount { get; }
        public int LossCount { get; }

        public SearchUsersAndDebatesUserItemView(
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
