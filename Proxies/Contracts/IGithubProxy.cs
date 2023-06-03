using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gh.Proxies.Contracts
{
    public interface IGithubProxy
    {
        Task<string> GetRepoContributors(string org, string repoName, CancellationToken cancellationToken);
    }
}