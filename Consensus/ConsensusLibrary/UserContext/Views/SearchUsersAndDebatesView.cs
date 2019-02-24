using System;
using System.Collections.Generic;
using System.Text;

namespace ConsensusLibrary.UserContext.Views
{
    public class SearchUsersAndDebatesView
    {
        public IEnumerable<SearchUsersAndDebatesDebateItemView> Debates { get; }
        public IEnumerable<SearchUsersAndDebatesUserItemView> Users { get; }

        public SearchUsersAndDebatesView(
            IEnumerable<SearchUsersAndDebatesDebateItemView> debates,
            IEnumerable<SearchUsersAndDebatesUserItemView> users)
        {
            Debates = debates;
            Users = users;
        }
    }
}
