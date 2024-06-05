using Newtonsoft.Json;

namespace VisitorSecurityClearanceSystem.DTO
{
    public class UserDTO
    {

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]

        public string Id { get; set; }

        [JsonProperty(PropertyName = "userName", NullValueHandling = NullValueHandling.Ignore)]

        public string Username { get; set; }

        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]

        public string Password { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }


        [JsonProperty(PropertyName = "phoneNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber { get; set; }
    }
}
