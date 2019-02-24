using System.Collections.Generic;

namespace Consensus.Models.SearchModels
{
    public class GetUserAndDebatesResponseModel
    {
        public IEnumerable<GetUserAndDebatesDebateResponseItemModel> Debates { get; }
        public IEnumerable<GetUserAndDebatesUserResponseItemModel> Users { get; }

        public GetUserAndDebatesResponseModel(
            IEnumerable<GetUserAndDebatesDebateResponseItemModel> debates,
            IEnumerable<GetUserAndDebatesUserResponseItemModel> users)
        {
            Debates = debates;
            Users = users;
        }
    }
}
