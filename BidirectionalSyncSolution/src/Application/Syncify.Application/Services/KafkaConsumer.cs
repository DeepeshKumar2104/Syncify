using Confluent.Kafka;
using Newtonsoft.Json;
using System;

namespace Syncify.Application.Services
{
    public class KafkaConsumer
    {
        private readonly string _bootstrapServers;
        private readonly string _topicName;

        public KafkaConsumer(string bootstrapServers, string topicName)
        {
            _bootstrapServers = bootstrapServers;
            _topicName = topicName;
        }

        public void ConsumeMessages()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = "employee-group",  // Consumer group ID
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(_topicName);

                while (true)
                {
                    try
                    {
                        var consumeResult = consumer.Consume();
                        var message = consumeResult.Message.Value;

                        // Deserialize the message to a dynamic object
                        var employeeData = JsonConvert.DeserializeObject<dynamic>(message);

                        // Print all data in a detailed format
                        Console.WriteLine("Employee Registration Information:\n");

                        // Print Employee Info
                        Console.WriteLine("Employee:");
                        Console.WriteLine($"  Employee ID: {employeeData.Employee.EmployeeId}");
                        Console.WriteLine($"  External Employee Code: {employeeData.Employee.ExternalEmployeeCode}");
                        Console.WriteLine($"  Name: {employeeData.Employee.FirstName} {employeeData.Employee.LastName}");
                        Console.WriteLine($"  Department: {employeeData.Employee.Department}");
                        Console.WriteLine($"  Designation: {employeeData.Employee.Designation}");
                        Console.WriteLine();

                        // Print Contact Info
                        Console.WriteLine("Contact:");
                        Console.WriteLine($"  Email: {employeeData.Contact.Email}");
                        Console.WriteLine($"  Phone: {employeeData.Contact.Phone}");
                        Console.WriteLine();

                        // Print Address Info
                        Console.WriteLine("Address:");
                        Console.WriteLine($"  Line 1: {employeeData.Address.Line1}");
                        Console.WriteLine($"  City: {employeeData.Address.City}");
                        Console.WriteLine($"  State: {employeeData.Address.State}");
                        Console.WriteLine($"  Zip Code: {employeeData.Address.ZipCode}");
                        Console.WriteLine();

                        // Print Project Info
                        Console.WriteLine("Project:");
                        Console.WriteLine($"  Project Name: {employeeData.Project.Name}");
                        Console.WriteLine($"  Description: {employeeData.Project.Description}");
                        Console.WriteLine();

                        // Print Employee Project Info
                        Console.WriteLine("Employee Project:");
                        Console.WriteLine($"  Assigned On: {employeeData.EmployeeProject.AssignedOn}");
                        Console.WriteLine();

                        // Print Audit Log Info
                        Console.WriteLine("Audit Log:");
                        Console.WriteLine($"  Action: {employeeData.AuditLog.Action}");
                        Console.WriteLine($"  Timestamp: {employeeData.AuditLog.Timestamp}");
                        Console.WriteLine("\n--------------------------------------\n");
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error: {e.Error.Reason}");
                    }
                }
            }
        }
    }
}
