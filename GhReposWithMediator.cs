using System.Net;
using gh.CQS.Queries.Repo;
using gh.Domain.Response;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class GhReposWithMediator
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public GhReposWithMediator(IMediator mediator, ILoggerFactory loggerFactory)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = loggerFactory.CreateLogger<GhStatus>();
        }

        [Function("GetGhReposContributorsWithMediator")]
        [OpenApiOperation(operationId: "GetGhReposContributorsWithMediator")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<ContributorResponse>), Description = "Get a list of Repo Contributors by org and repo name")]
        public async Task<HttpResponseData> GetRepoContributorsAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "gh/v1/mediator/repos/{org}/{repo}/contributors")] HttpRequestData req, string org, string repo, CancellationToken cancellationToken)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            try {
                var res = await _mediator.Send(new GetRepoContributorsQuery(org, repo));
                await response.WriteAsJsonAsync(res);
            } 
            catch (Exception e) {
                _logger.LogError("error occured: " + e.Message, e);
                await response.WriteAsJsonAsync(new {error = e.Message}, HttpStatusCode.BadRequest);
            }
            return response;
        }

        [Function("GetGhReposPullsWithMediator")]
        [OpenApiOperation(operationId: "GetGhReposPullsWithMediator")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<PullsResponse>), Description = "Get a list of Pulls for a repo by org and repo name")]
        public async Task<HttpResponseData> GetRepoPullsAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "gh/v1/mediator/repos/{org}/{repo}/pulls")] HttpRequestData req, string org, string repo, CancellationToken cancellationToken)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            try {
                var res = await _mediator.Send(new GetRepoPullsQuery(org, repo));
                await response.WriteAsJsonAsync(res);
            } 
            catch (Exception e) {
                _logger.LogError("error occured: " + e.Message, e);
                await response.WriteAsJsonAsync(new {error = e.Message}, HttpStatusCode.BadRequest);
            }
            return response;
        }

        // TODO: support additional github api endpoints
    }
}
