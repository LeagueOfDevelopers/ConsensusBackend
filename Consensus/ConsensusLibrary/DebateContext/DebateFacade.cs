using System;
using ConsensusLibrary.DebateContext.Views;
using ConsensusLibrary.Tools;
using ConsensusLibrary.UserContext;
using EnsureThat;
using System.Linq;

namespace ConsensusLibrary.DebateContext
{
    public class DebateFacade : IDebateFacade
    {
        public DebateFacade(
            IUserRepository userRepository,
            IDebateRepository debateRepository)
        {
            _userRepository = Ensure.Any.IsNotNull(userRepository);
            _debateRepository = Ensure.Any.IsNotNull(debateRepository);
        }

        public Identifier CreateDebate(DateTimeOffset startDateTime, DateTimeOffset endDateTime,
            string title, Identifier inviter, Identifier invited, DebateCategory debateCategory)
        {
            Ensure.Any.IsNotNull(inviter);
            Ensure.Any.IsNotNull(invited);

            var inviterUser = _userRepository.GetUserById(inviter);
            var InvitedUser = _userRepository.GetUserById(invited);

            var newDebates = new Debate(startDateTime, endDateTime, title, inviter, invited, debateCategory);

            _debateRepository.AddDebate(newDebates);

            return newDebates.Identifier;
        }

        public DebateView GetDebate(Identifier identifier)
        {
            Ensure.Any.IsNotNull(identifier);

            var debate = _debateRepository.GetDebate(identifier);

            var opponents = debate.Members.Where(m => m.MemberRole == MemberRole.Opponent).ToList();
            var viewers = debate.Members.Where(m => m.MemberRole == MemberRole.Viewer).ToList();

            var leftOpponent = _userRepository.GetUserById(opponents[0].UserIdentifier);
            var rightOpponent = _userRepository.GetUserById(opponents[1].UserIdentifier);

            var result = new DebateView(debate.Identifier, leftOpponent.Credentials.NickName, leftOpponent.Identifier,
                rightOpponent.Credentials.NickName, rightOpponent.Identifier, debate.StartDateTime, debate.EndDateTime, viewers.Count,
                debate.Title, debate.DebateCategory);

            return result;
        }

        private readonly IUserRepository _userRepository;
        private readonly IDebateRepository _debateRepository;

    }
}
