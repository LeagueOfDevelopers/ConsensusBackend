using System;
using EnsureThat;

namespace Consensus.Security
{
    public class PaginationSettings
    {
        public int PageSize { get; }

        public PaginationSettings(int pageSize)
        {
            Ensure.Bool.IsTrue(pageSize > 0, nameof(pageSize),
                opt => opt.WithException(new ArgumentException()));

            PageSize = pageSize;
        }
    }
}
