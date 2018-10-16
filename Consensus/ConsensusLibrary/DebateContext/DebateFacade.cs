using System;
using System.Collections.Generic;
using System.Linq;
using ConsensusLibrary.DebateContext.Views;
using ConsensusLibrary.Tools;
using ConsensusLibrary.UserContext;
using EnsureThat;

namespace ConsensusLibrary.DebateContext
{
    public class DebateFacade : IDebateFacade
    {
        private readonly IDebateRepository _debateRepository;
        private readonly DebateSettings _debateSettings;

        private readonly IUserRepository _userRepository;

        public DebateFacade(
            IUserRepository userRepository,
            IDebateRepository debateRepository,
            DebateSettings debateSettings)
        {
            _userRepository = Ensure.Any.IsNotNull(userRepository);
            _debateRepository = Ensure.Any.IsNotNull(debateRepository);
            _debateSettings = Ensure.Any.IsNotNull(debateSettings);
        }

        public Identifier CreateDebate(DateTimeOffset startDateTime,
            string title, Identifier inviter, Identifier invited, DebateCategory debateCategory)
        {
            Ensure.Any.IsNotNull(inviter);
            Ensure.Any.IsNotNull(invited);

            var inviterUser = _userRepository.GetUserById(inviter);
            var InvitedUser = _userRepository.GetUserById(invited);

            var newDebates = new Debate(startDateTime, title, inviter, invited, debateCategory);

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
                rightOpponent.Credentials.NickName, rightOpponent.Identifier, debate.StartDateTime,
                viewers.Count,
                debate.Title, debate.DebateCategory);

            return result;
        }

        public IEnumerable<LiveDebateView> GetLiveDebates()
        {
            var debates = _debateRepository.GetActualDebatesForInterval(_debateSettings.DebateMinutesDuration);
            var result = new List<LiveDebateView>();
            debates.ToList().ForEach(d => {
                var opponents = d.Members.Where(m => m.MemberRole == MemberRole.Opponent).ToList();
                var viewers = d.Members.Where(m => m.MemberRole == MemberRole.Viewer).ToList();
                var leftOpponent = _userRepository.GetUserById(opponents[0].UserIdentifier);
                var rightOpponent = _userRepository.GetUserById(opponents[1].UserIdentifier);
                result.Add(new LiveDebateView(d.Identifier, d.Title, leftOpponent.Identifier, rightOpponent.Identifier,
                    leftOpponent.Credentials.NickName, rightOpponent.Credentials.NickName, viewers.Count,
                    d.DebateCategory, null));
            });

            return result;
        }
    }
}