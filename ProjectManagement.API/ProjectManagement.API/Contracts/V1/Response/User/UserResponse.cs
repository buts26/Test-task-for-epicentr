using ProjectManagement.API.Contracts.V1.Response.Project;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProjectManagement.API.Contracts.V1.Response.User
{
    
    public class UserResponse
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ProjectResponse> Projects { get; set; }
    }
}
