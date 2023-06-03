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
            /*
                              request.Headers.TryAddWithoutValidation("Accept", "application/vnd.github+json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer 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");
                    request.Headers.TryAddWithoutValidation("X-GitHub-Api-Version", "2022-11-28"); 
                    request.Headers.TryAddWithoutValidation("User-Agent", "my test app"); 
            */
            Token = GH_TOKEN;
            var headers = new Dictionary<string, string> {
                {"X-GitHub-Api-Version" , "2022-11-28"},
                {"Accept", "application/vnd.github+json"},
                {"User-Agent" , "my test app"}
            };
            /*var headers2 = new {
                X-GitHub-Api-Version = "2022-11-28",
                Accept = "application/vnd.github+json",
                User-Agent = "my test app"
            };*/
            //var objHeader = ConvertDictionaryTo<object>(headers);
            //Headers = headers2;//;objHeader;
            var seg = $"repos/{org}/{repoName}/contributors";
            //
            var response = await GetCAsync(seg);
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

        private T ConvertDictionaryTo<T>(IDictionary<string, string> dictionary) where T : new()
        {
            Type type = typeof (T);
            T ret = new T();
            foreach (var keyValue in dictionary)
                type.GetProperty(keyValue.Key).SetValue(ret, keyValue.Value, null);
            //
            return ret;
        }

        
    }
}