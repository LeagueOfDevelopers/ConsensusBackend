using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsensusLibrary.BackgroundProcessService;
using ConsensusLibrary.CategoryContext;
using ConsensusLibrary.CategoryContext.Exceptions;
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
        private readonly IBackgroundProcessService _backgroundProcessService;
        private readonly ICategoryRepository _categoryRepository;

        private readonly IUserRepository _userRepository;

        public DebateFacade(
            IUserRepository userRepository,
            IDebateRepository debateRepository,
            DebateSettings debateSettings,
            IBackgroundProcessService backgroundProcessService,
            ICategoryRepository categoryRepository)
        {
            _userRepository = Ensure.Any.IsNotNull(userRepository);
            _debateRepository = Ensure.Any.IsNotNull(debateRepository);
            _debateSettings = Ensure.Any.IsNotNull(debateSettings);
            _backgroundProcessService = Ensure.Any.IsNotNull(backgroundProcessService);
            _categoryRepository = Ensure.Any.IsNotNull(categoryRepository);
        }

        public Identifier CreateDebate(DateTimeOffset startDateTime,
            string title, Identifier inviter, Identifier invited, string debateCategory)
        {
            Ensure.Any.IsNotNull(inviter);
            Ensure.Any.IsNotNull(invited);

            var inviterUser = _userRepository.GetUserById(inviter);
            var invitedUser = _userRepository.GetUserById(invited);

            var currentCategory = _categoryRepository.GetCategoryByTitle(debateCategory);

            var newDebates = new Debate(startDateTime, title, inviter, invited,
                currentCategory, _debateSettings.RoundCount, _debateSettings.RoundLength);

            _debateRepository.AddDebate(newDebates);

            _backgroundProcessService.DelayedCheckDebateOverdue(newDebates.Identifier, startDateTime);

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
                debate.Title, debate.DebateCategory.CategoryTitle, debate.State, membersView);

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
                    d.DebateCategory.CategoryTitle, null));
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