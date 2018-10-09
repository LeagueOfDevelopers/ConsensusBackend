using System;

namespace Consensus.Security
{
    public interface IJwtIssuer
    {
        string IssueJwt(string role, Guid id);
    }
}
