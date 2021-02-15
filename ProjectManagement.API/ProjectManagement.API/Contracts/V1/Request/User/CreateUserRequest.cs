using ProjectManagement.BLL.Models.Project.Request;
using System.Collections.Generic;

namespace ProjectManagement.API.Contracts.V1.Request.User
{
    public class CreateUserRequest
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public List<ProjectRequest> Projects { get; set; } = new List<ProjectRequest>();
    }
}
