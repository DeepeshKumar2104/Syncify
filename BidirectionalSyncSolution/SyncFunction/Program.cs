using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.EventHubs;
using Microsoft.Azure.WebJobs;
using Azure.Messaging.EventHubs;

namespace SyncFunction
{
    class Program
    {
        static void Main(string[] args)
        {
           /* // Build the host for the Azure Functions runtime
            var host = new HostBuilder()
                .ConfigureWebJobs(builder =>
                {
                    builder.AddEventHubs();  // Add EventHub extension for processing events
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();  // Log to console for debugging
                })
                .Build();

            // Run the function app
            host.Run();*/
        }
    }
}
