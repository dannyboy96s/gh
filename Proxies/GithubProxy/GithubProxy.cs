using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<string> GetRepoContributors(string org, string repoName, CancellationToken cancellationToken)
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
            // Make the call 
            var response = await GetAsync<string>(seg, null, 30, cancellationToken);
            //
            return response;
        }

         protected async Task<string> GetCAsync(string segment){
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{Url}{segment}"))
                {
                    request.Headers.TryAddWithoutValidation("Accept", "application/vnd.github+json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {Token}");
                    request.Headers.TryAddWithoutValidation("X-GitHub-Api-Version", "2022-11-28"); 
                    request.Headers.TryAddWithoutValidation("User-Agent", "my test app"); 

                    var resp = await httpClient.SendAsync(request);
                    if(resp.IsSuccessStatusCode) {
                        return await resp.Content.ReadAsStringAsync();

                    }
                    throw new Exception("http error:"+ resp.StatusCode);
                    
                }
            }
        }
    }
}