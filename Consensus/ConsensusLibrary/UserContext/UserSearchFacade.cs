using System;
using System.Collections.Generic;
using System.Linq;
using ConsensusLibrary.CategoryContext;
using ConsensusLibrary.CategoryContext.Exceptions;
using ConsensusLibrary.DebateContext;
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

        public SearchUsersAndDebatesView SearchUsersAndDebates(
            string sectionName,
            string category,
            bool isLive,
            int pageSize,
            int debatePageNumber,
            int userPageNumber)
        {
            Ensure.String.IsNotNullOrWhiteSpace(sectionName);
            Ensure.String.IsNotNullOrWhiteSpace(category);
            Ensure.Bool.IsTrue(_categoryRepository.DoesCategoryExist(category),
                nameof(category), opt => opt.WithException(new CategoryNotFoundException()));

            var selectedUsers = _userRepository.GetUsersByName(sectionName, pageSize, userPageNumber);
            var selectedDebates = _debateRepository.SearchDebate(
                sectionName,
                category,
                isLive,
                pageSize,
                debatePageNumber);

            var debateViews = new List<SearchUsersAndDebatesDebateItemView>();

            selectedDebates.ToList()
                .ForEach(d =>
                {
                    var inviterMember = d.Members.FirstOrDefault();
                    var inviterUser = _userRepository.GetUserById(inviterMember.UserIdentifier);
                    var invitedMember = d.Members.Skip(1).FirstOrDefault();
                    var invitedUser = _userRepository.GetUserById(invitedMember.UserIdentifier);
                    var isLiveDebate = d.StartDateTime > DateTimeOffset.UtcNow && d.EndDateTime < DateTimeOffset.UtcNow;

                    debateViews.Add(new SearchUsersAndDebatesDebateItemView(
                        d.Title, d.Identifier.Id, isLiveDebate, d.DebateCategory.CategoryTitle,
                        inviterUser.Credentials.NickName, inviterUser.Identifier.Id,
                        invitedUser.Credentials.NickName, invitedUser.Identifier.Id));
                });

            var userViews = new List<SearchUsersAndDebatesUserItemView>();

            //TODO win and loss counting
            selectedUsers.ToList().ForEach(u => userViews.Add(
                new SearchUsersAndDebatesUserItemView(
                    u.Credentials.NickName, u.Identifier.Id, u.UserProfile.AvatarLink, 0, 0)));

            var result = new SearchUsersAndDebatesView(debateViews, userViews);

            return result;
        }

        private readonly IUserRepository _userRepository;
        private readonly IDebateRepository _debateRepository;
        private readonly ICategoryRepository _categoryRepository;

        public UserSearchFacade(
            IUserRepository userRepository,
            IDebateRepository debateRepository,
            ICategoryRepository categoryRepository)
        {
            _userRepository = Ensure.Any.IsNotNull(userRepository);
            _debateRepository = Ensure.Any.IsNotNull(debateRepository);
            _categoryRepository = Ensure.Any.IsNotNull(categoryRepository);
        }
    }
}
