using Newtonsoft.Json;
using VisitorSecurityClearanceSystem.Common;

namespace VisitorSecurityClearanceSystem.Model
{
    public class Visitor : BaseEntity
    {

        


        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "phoneNiumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "visitingTo", NullValueHandling = NullValueHandling.Ignore)]
        public string VisitingTo { get; set; }

        [JsonProperty(PropertyName = "purpose", NullValueHandling = NullValueHandling.Ignore)]
        public string Purpose { get; set; }

        [JsonProperty(PropertyName = "entryTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime EntryTime { get; set; }

        [JsonProperty(PropertyName = "exitTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ExitTime { get; set; }

        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; } // Pending, Approved, Rejected

        [JsonProperty(PropertyName = "company", NullValueHandling = NullValueHandling.Ignore)]
        public string Company { get; set; }

    }

}
