using EmployeeManagementSystem.Comman;
using EmployeeManagementSystem.DTO;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.Entity
{
    public class EmployeeBasicDetailsEntity: BaseEntity
    {

        [JsonProperty(PropertyName = "salutory", NullValueHandling = NullValueHandling.Ignore)]

        public string Salutory { get; set; }

        [JsonProperty(PropertyName = "firstName", NullValueHandling = NullValueHandling.Ignore)]

        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "middleName", NullValueHandling = NullValueHandling.Ignore)]

        public string MiddleName { get; set; }

        [JsonProperty(PropertyName = "lastName", NullValueHandling = NullValueHandling.Ignore)]

        public string LastName { get; set; }

        [JsonProperty(PropertyName = "nickName", NullValueHandling = NullValueHandling.Ignore)]

        public string NickName { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]

        public string Email { get; set; }

        [JsonProperty(PropertyName = "mobile", NullValueHandling = NullValueHandling.Ignore)]

        public string Mobile { get; set; }

        [JsonProperty(PropertyName = "employeeId", NullValueHandling = NullValueHandling.Ignore)]

        public string EmployeeID { get; set; }

        [JsonProperty(PropertyName = "role", NullValueHandling = NullValueHandling.Ignore)]

        public string Role { get; set; }

        [JsonProperty(PropertyName = "reportingManagerUid", NullValueHandling = NullValueHandling.Ignore)]

        public string ReportingManagerUId { get; set; }

        [JsonProperty(PropertyName = "reportingManagerName", NullValueHandling = NullValueHandling.Ignore)]

        public string ReportingManagerName { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]

        public Address Address { get; set; }

        [JsonProperty(PropertyName = "dateOfBirth", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty(PropertyName = "dateOfJoining", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime DateOfJoining { get; set; }

        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Status { get; set; }
    }

    public class EmployeeBasicFilterCriteria
    {
        public EmployeeBasicFilterCriteria()
        {
            Filters = new List<FilterCriteria>();
            Employees = new List<EmployeeBasicDetailsEntity>();

        }

        public int Page { get; set; }
       
        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public List<FilterCriteria> Filters { get; set; }
        public List<EmployeeBasicDetailsEntity> Employees { get; set; }

    }

    public class FilterCriteria
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
    }
}
