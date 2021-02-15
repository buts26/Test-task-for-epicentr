using ProjectManagement.BLL.Models.Project.Request;
using System.Collections.Generic;

namespace ProjectManagement.API.Configuration
{
    public class ProjectDataConfiguration
    {
        public List<ProjectRequest> Projects { get; set; } = new List<ProjectRequest>();
    }
}