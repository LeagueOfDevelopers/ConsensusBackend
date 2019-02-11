using System.Collections.Generic;

namespace Consensus.Models.SearchModels
{
    public class GetUserBySectionNameResponseModel
    {
        public IEnumerable<GetUserBySectionNameResponseItemModel> Users { get; }

        public GetUserBySectionNameResponseModel(
            IEnumerable<GetUserBySectionNameResponseItemModel> users)
        {
            Users = users;
        }
    }
}
