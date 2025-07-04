namespace Syncify.Application.Models
{
    public class NewModels
    {
        public EmployeeModel Employee { get; set; }
        public EmployeeProjectModel EmployeeProject { get; set; }
        public EmployeeAddressModel Address { get; set; }
        public EmployeeContactModel Contact { get; set; }
    }

    // Model for creating an employee
    public class EmployeeModel
    {
        public string ExternalEmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentID { get; set; }
        public int DesignationID { get; set; }
    }

    // Model for assigning employee to a project
    public class EmployeeProjectModel
    {
        public int EmployeeID { get; set; }
        public int ProjectID { get; set; }
        public DateTime AssignedOn { get; set; }
    }

    // Model for creating an address for an employee
    public class EmployeeAddressModel
    {
        public int EmployeeID { get; set; }
        public string Line1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    // Model for creating a contact for an employee
    public class EmployeeContactModel
    {
        public int EmployeeID { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
