using EmployeeManagementSystem.Entity;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.DTO
{
    public class EmployeeAdditionDetailsDTO
    {

        [JsonProperty(PropertyName = "employeeBasicDetails", NullValueHandling = NullValueHandling.Ignore)]

        public string EmployeeBasicDetailsUId { get; set; }

        [JsonProperty(PropertyName = "alternateEmail", NullValueHandling = NullValueHandling.Ignore)]

        public string AlternateEmail { get; set; }

        [JsonProperty(PropertyName = "alternateMobile", NullValueHandling = NullValueHandling.Ignore)]

        public string AlternateMobile { get; set; }

        [JsonProperty(PropertyName = "workInformation", NullValueHandling = NullValueHandling.Ignore)]

        public WorkInformation WorkInformation { get; set; }

        [JsonProperty(PropertyName = "personalDetails", NullValueHandling = NullValueHandling.Ignore)]

        public PersonalDetails PersonalDetails { get; set; }

        [JsonProperty(PropertyName = "identityInformation", NullValueHandling = NullValueHandling.Ignore)]

        public IdentityInformation IdentityInformation { get; set; }
    }

}
