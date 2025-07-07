using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventHubs;
using Newtonsoft.Json;
using System.Text;


namespace Syncify.Functions
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [FunctionName("ConsumeEventHubMessages")]
        public void Run(
            // EventHubTrigger to consume messages from Azure Event Hub (employeeevents)
            [EventHubTrigger("employeeevents", Connection = "", ConsumerGroup = "")]
            EventData[] events,
            ILogger log)
        {
            foreach (EventData @event in events)
            {
                string eventBody = Encoding.UTF8.GetString(@event.Body.ToArray());
                log.LogInformation($"Received Event Body: {eventBody}");

                try
                {
                    var employeeData = JsonConvert.DeserializeObject<dynamic>(eventBody);
                    log.LogInformation($"Employee ID: {employeeData.EmployeeId}");
                    log.LogInformation($"First Name: {employeeData.FirstName}");
                    log.LogInformation($"Last Name: {employeeData.LastName}");
                    log.LogInformation($"Department: {employeeData.Department}");
                    log.LogInformation($"Designation: {employeeData.Designation}");

                    // Further processing (e.g., inserting data into a database)
                }
                catch (JsonReaderException ex)
                {
                    log.LogError($"Error deserializing message: {ex.Message}");
                }
            }
        }
    }
}
