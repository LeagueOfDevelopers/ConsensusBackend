using System.Collections.Generic;
using System.Linq;
using ConsensusLibrary.DebateContext.Views;
using ConsensusLibrary.Tools;
using ConsensusLibrary.UserContext;
using EnsureThat;

namespace ConsensusLibrary.DebateContext
{
    public class ChatFacade : IChatFacade
    {
        private readonly IDebateRepository _debateRepository;
        private readonly IUserRepository _userRepository;

        public ChatFacade(IDebateRepository debateRepository, IUserRepository userRepository)
        {
            _debateRepository = Ensure.Any.IsNotNull(debateRepository);
            _userRepository = Ensure.Any.IsNotNull(userRepository);
        }

        public IEnumerable<MessageView> GetMessages(Identifier debateId)
        {
            Ensure.Any.IsNotNull(debateId);

            var currentDebate = _debateRepository.GetDebate(debateId);

            var result = new List<MessageView>();

            currentDebate.Messages.ToList().ForEach(m =>
            {
                var currentUser = _userRepository.GetUserById(m.UserId);
                result.Add(new MessageView(m.Id, m.UserId, m.SentOn, m.Text, currentUser.Credentials.NickName));
            });

            return result;
        }

        public Identifier SendMessage(Identifier userId, Identifier debateId, string text)
        {
            Ensure.Any.IsNotNull(userId);
            Ensure.Any.IsNotNull(debateId);

            var currentDebate = _debateRepository.GetDebate(debateId);
            var currentUser = _userRepository.GetUserById(userId);

            var addingMessage = new Message(currentUser.Identifier, debateId, text);

            currentDebate.AddMessage(addingMessage);

            _debateRepository.SendMessage(addingMessage);

            return addingMessage.Id;
        }
    }
}