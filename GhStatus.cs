using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class GhStatus
    {
        private readonly ILogger _logger;

        public GhStatus(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GhStatus>();
        }

        [Function("GhStatus")]
        [OpenApiOperation(operationId: "GhStatus")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "Status endpoint")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "gh/v1/status")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Healthy");

            return response;
        }
    }
}
