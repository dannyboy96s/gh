using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gh.Domain.Repo;

namespace gh.Proxies.Contracts
{
    public interface IGithubProxy
    {
        Task<List<Contributor>> GetRepoContributors(string org, string repoName, CancellationToken cancellationToken);
        Task<List<Pull2>> GetRepoPulls(string org, string repoName, CancellationToken cancellationToken);
    }
}