using ProjectManagement.API.Contracts.V1.Request.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProjectManagement.BLL.Models.User.Request;

namespace ProjectManagement.API.Contracts.V1.Request.Project
{
    public class CreateProjectRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Descriptions { get; set; }
        public List<UserRequest> Users { get; set; }
    }
}
