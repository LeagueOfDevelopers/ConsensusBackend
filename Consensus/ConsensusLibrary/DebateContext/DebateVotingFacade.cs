using System.Linq;
using ConsensusLibrary.DebateContext.Views;
using ConsensusLibrary.Tools;
using ConsensusLibrary.UserContext;
using EnsureThat;

namespace ConsensusLibrary.DebateContext
{
    public class DebateVotingFacade : IDebateVotingFacade
    {
        private readonly IDebateRepository _debateRepository;
        private readonly IUserRepository _userRepository;

        public DebateVotingFacade(
            IDebateRepository debateRepository,
            IUserRepository userRepository)
        {
            _debateRepository = Ensure.Any.IsNotNull(debateRepository);
            _userRepository = Ensure.Any.IsNotNull(userRepository);
        }

        public void Vote(Identifier debate, Identifier fromUser, Identifier toUser)
        {
            var voter = _userRepository.GetUserById(fromUser);
            var voted = _userRepository.GetUserById(toUser);

            var currentDebate = _debateRepository.GetDebate(debate);

            currentDebate.MakeVote(fromUser, toUser);
        }

        public DebateVotingView GetVotingResults(Identifier debateId)
        {
            var currentDebate = _debateRepository.GetDebate(debateId);

            var opponents = currentDebate.Members.Where(m => m.MemberRole == MemberRole.Opponent).ToList();
            var leftOpponent = _userRepository.GetUserById(opponents[0].UserIdentifier);
            var rightOpponent = _userRepository.GetUserById(opponents[1].UserIdentifier);
            var leftOpponentVotes = currentDebate.Votes.ToList().Count(v => v.ToUser == leftOpponent.Identifier);
            var rightOpponentVotes = currentDebate.Votes.ToList().Count(v => v.ToUser == rightOpponent.Identifier);

            return new DebateVotingView(leftOpponentVotes, leftOpponent.Credentials.NickName,
                leftOpponent.Identifier.Id, rightOpponentVotes, rightOpponent.Credentials.NickName,
                rightOpponent.Identifier.Id);
        }
    }
}