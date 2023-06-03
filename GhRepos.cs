using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class GhRepos
    {
        private readonly ILogger _logger;

        public GhRepos(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GhStatus>();
        }

        [Function("GetGhReposContributors")]
        [OpenApiOperation(operationId: "GetGhReposContributors")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "Get Repo Contributors by org and repo name")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "gh/v1/repos/{org}/contributors/{repo}")] HttpRequestData req, string org, string repo, CancellationToken cancellationToken)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Healthy");

            return response;
        }
    }
}
