using System;
using ProjectManagement.BLL.Models.Project.Request;
using System.Collections.Generic;

namespace ProjectManagement.BLL.Models.User.Request
{
    public class UserRequest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        public List<ProjectRequest> Projects { get; set; } = new List<ProjectRequest>();
    }
}
