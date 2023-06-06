using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gh.Domain.Repo;
using gh.Proxies.Contracts;

namespace gh.Proxies.GithubProxy
{
    public class GithubProxy : BaseRestClient, IGithubProxy
    {
        private readonly string GH_TOKEN = Environment.GetEnvironmentVariable("GH_TOKEN") ?? throw new ArgumentNullException("Missing / incorrect GH token in settings");
        private readonly string _baseUrl;

        public GithubProxy(string baseUrl) : base (baseUrl)
        {  
        }

        public async Task<List<Contributor>> GetRepoContributors(string org, string repoName, CancellationToken cancellationToken)
        {
            // Set the token
            Token = GH_TOKEN;
            // Set the headers per github requirement 
            var headers = new Dictionary<string, string> {
                {"X-GitHub-Api-Version" , "2022-11-28"},
                {"Accept", "application/vnd.github+json"},
                {"User-Agent" , "my test app"}
            };
            Headers = headers;
            // Set the segment url
            var seg = $"repos/{org}/{repoName}/contributors";
            // Make the http call
            var response = await GetAsync<List<Contributor>>(seg, null, 30, cancellationToken);
            //
            return response;
        }

        public async Task<List<Pull>> GetRepoPulls(string org, string repoName, CancellationToken cancellationToken)
        {
            // Set the token
            Token = GH_TOKEN;
            // Set the headers per github requirement 
            var headers = new Dictionary<string, string> {
                {"X-GitHub-Api-Version" , "2022-11-28"},
                {"Accept", "application/vnd.github+json"},
                {"User-Agent" , "my test app"}
            };
            Headers = headers;
            // Set the segment url
            var seg = $"repos/{org}/{repoName}/pulls";
            // Make the http call
            var response = await GetAsync<List<Pull>>(seg, null, 30, cancellationToken);
            //
            return response;
        }
    }
}