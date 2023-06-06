using System.Net;
using gh.Domain.Response;
using gh.Proxies.Contracts;
using Mapster;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class GhRepos
    {
        private readonly ILogger _logger;
        private readonly IGithubProxy _proxy;

        public GhRepos(IGithubProxy proxy, ILoggerFactory loggerFactory)
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
            _logger = loggerFactory.CreateLogger<GhStatus>();
        }

        [Function("GetGhReposContributors")]
        [OpenApiOperation(operationId: "GetGhReposContributors")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "Get Repo Contributors by org and repo name")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "gh/v1/repos/{org}/{repo}/contributors")] HttpRequestData req, string org, string repo, CancellationToken cancellationToken)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            try {
                var contributors = await _proxy.GetRepoContributors(org, repo, cancellationToken);
                var result = new List<ContributorResponse>();
                if (contributors is not null)
                    foreach (var contributor in contributors)
                        // map to ContributorResponse model and add to result list
                        result.Add(contributor.Adapt<ContributorResponse>());
                //
                await response.WriteAsJsonAsync(result);
            } 
            catch (Exception e) {
                _logger.LogError("error occured: " + e.Message, e);
                await response.WriteAsJsonAsync(new {error = e.Message}, HttpStatusCode.BadRequest);
            }
            return response;
        }
    }
}
