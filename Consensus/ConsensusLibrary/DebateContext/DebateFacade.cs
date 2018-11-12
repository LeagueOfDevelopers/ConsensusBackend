using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsensusLibrary.DebateContext.Exceptions;
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
            var invitedUser = _userRepository.GetUserById(invited);

            var newDebates = new Debate(startDateTime, title, inviter, invited,
                debateCategory, _debateSettings.RoundCount, _debateSettings.RoundLength);

            _debateRepository.AddDebate(newDebates);

            return newDebates.Identifier;
        }

        public DebateView GetDebate(Identifier identifier)
        {
            Ensure.Any.IsNotNull(identifier);

            var debate = _debateRepository.GetDebate(identifier);

            var opponents = debate.Members.Where(m => m.MemberRole == MemberRole.Opponent).ToList();

            var membersView = new List<DebateMemberView>();

            opponents.ForEach(m =>
            {
                var currentUser = _userRepository.GetUserById(m.UserIdentifier);
                membersView.Add(new DebateMemberView(currentUser.Credentials.NickName,
                    m.UserIdentifier, m.Ready, m.TranslationLink));
            });

            var result = new DebateView(debate.Identifier, debate.StartDateTime,
                debate.Title, debate.DebateCategory, debate.State, membersView);

            return result;
        }

        public IEnumerable<LiveDebateView> GetLiveDebates()
        {
            var debates = _debateRepository.GetActualDebates();
            var result = new List<LiveDebateView>();
            debates.ToList().ForEach(d =>
            {
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

        public void SetReadyStatus(Identifier debateId, Identifier userId)
        {
            Ensure.Any.IsNotNull(debateId);
            Ensure.Any.IsNotNull(userId);

            var debate = _debateRepository.GetDebate(debateId);
            var currentUser = _userRepository.GetUserById(userId);

            debate.SetReadyStatus(userId);

            if (debate.State == DebateState.Approved)
                Task.Delay(10000).ContinueWith(f => CloseTranslationThread()); // TODO config values

            _debateRepository.UpdateDebate(debate);
        }


        private void CloseTranslationThread()
        {
            // do stuff
        }
    }
}