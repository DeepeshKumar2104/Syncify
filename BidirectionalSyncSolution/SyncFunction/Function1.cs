using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Worker;

namespace SyncFunction
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        // Event Hub Trigger for In-Process Function
        [FunctionName("Function1")]
        public void Run(
            [EventHubTrigger("employeeevents", Connection = "EventHubConnectionString")] EventData[] events,
            ILogger log)
        {
            foreach (EventData @event in events)
            {
                var eventBody = @event.Body.ToString();  // Convert byte array to string (JSON)
                var eventContentType = @event.ContentType;

                // Log the event data
                log.LogInformation($"Event Body: {eventBody}");
                log.LogInformation($"Event Content-Type: {eventContentType}");

                // Further processing logic (e.g., saving data to SQL)
            }
        }
    }
}
