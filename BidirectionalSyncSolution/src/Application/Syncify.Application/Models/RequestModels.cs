namespace Syncify.Application.Models
{
    public class RequestModels
    {
        public DepartmentRequestModel Department { get; set; }
        public DesignationRequestModel Designation { get; set; }
        public ProjectRequestModel Project { get; set; }
        public EmployeeRequestModel Employee { get; set; }
        public ContactRequestModel Contact { get; set; }
        public AddressRequestModel Address { get; set; }
        public EmployeeProjectRequestModel EmployeeProject { get; set; }
        public AuditLogRequestModel AuditLog { get; set; }
    }

    // Request model for creating a department
    public class DepartmentRequestModel
    {
        public string Name { get; set; }
    }

    // Request model for creating a designation
    public class DesignationRequestModel
    {
        public string Title { get; set; }
    }

    // Request model for creating a project
    public class ProjectRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    // Request model for creating an employee
    public class EmployeeRequestModel
    {
        public string ExternalEmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentID { get; set; } // Reference to Department
        public int DesignationID { get; set; } // Reference to Designation
    }

    // Request model for creating a contact for an employee
    public class ContactRequestModel
    {
        public int EmployeeID { get; set; } // Reference to Employee
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    // Request model for creating an address for an employee
    public class AddressRequestModel
    {
        public int EmployeeID { get; set; } // Reference to Employee
        public string Line1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    // Request model for assigning employee to a project
    public class EmployeeProjectRequestModel
    {
        public int EmployeeID { get; set; } // Reference to Employee
        public int ProjectID { get; set; }  // Reference to Project
        public DateTime AssignedOn { get; set; }
    }

    // Request model for logging audit events for employees
    public class AuditLogRequestModel
    {
        public int EmployeeID { get; set; } // Reference to Employee
        public string Action { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
