using System;

namespace ConsensusLibrary.UserContext.Views
{
    public class SearchUsersAndDebatesDebateItemView
    {
        public string DebateTitle { get; }
        public Guid DebateIdentifier { get; }
        public bool IsLive { get; }
        public string Category { get; }
        public string InviterNickName { get; }
        public Guid InviterIdentifier { get; }
        public string InvitedNickName { get; }
        public Guid InvitedIdentifier { get; }

        public SearchUsersAndDebatesDebateItemView(
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
