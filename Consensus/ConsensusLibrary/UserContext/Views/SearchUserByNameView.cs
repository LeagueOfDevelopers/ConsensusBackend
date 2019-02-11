using System.Collections.Generic;

namespace ConsensusLibrary.UserContext.Views
{
    public class SearchUserByNameView
    {
        public IEnumerable<SearchUserByNameItemView> Users { get; }

        public SearchUserByNameView(IEnumerable<SearchUserByNameItemView> users)
        {
            Users = users;
        }
    }
}
