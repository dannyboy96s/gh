using System;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class GetWeeklyContributorReport
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public GetWeeklyContributorReport(IMediator mediator, ILoggerFactory loggerFactory)
        {
             _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = loggerFactory.CreateLogger<GetWeeklyContributorReport>();
        }

        [Function("GetWeeklyContributorReport")]
        public void Run([TimerTrigger("0 */5 * * * *")] MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            // get the contributions
            // possible return options....
            // upload them somewhere cosmosdb? blob?
            // or send an email?
            // or return a zip file?
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
