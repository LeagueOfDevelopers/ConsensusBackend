using System.Collections.Generic;
using System.Linq;
using ConsensusLibrary.UserContext.Views;
using EnsureThat;

namespace ConsensusLibrary.UserContext
{
    public class UserSearchFacade : IUserSearchFacade
    {
        public SearchUserByNameView SearchUserByName(string nameSection, int pageSize, int pageNumber)
        {
            Ensure.String.IsNotNullOrWhiteSpace(nameSection);

            var appropriateUsers = _userRepository.GetUsersByName(nameSection, pageSize, pageNumber);

            var userViewList = new List<SearchUserByNameItemView>();

            appropriateUsers.ToList().ForEach(u => 
                userViewList.Add(new SearchUserByNameItemView(u.Credentials.NickName, u.Identifier)));

            var result = new SearchUserByNameView(userViewList);

            return result;
        }

        private readonly IUserRepository _userRepository;

        public UserSearchFacade(IUserRepository userRepository)
        {
            _userRepository = Ensure.Any.IsNotNull(userRepository);
        }
    }
}
