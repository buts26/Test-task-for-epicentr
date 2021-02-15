using ProjectManagement.BLL.Models.Project.Response;
using System.Collections.Generic;

namespace ProjectManagement.BLL.Models.User.Response
{
    public class UserResult
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        public List<ProjectResult> Projects { get; set; }
    }
}
