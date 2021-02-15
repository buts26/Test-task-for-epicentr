using ProjectManagement.BLL.Models.User.Request;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.API.Contracts.V1.Request.Project
{
    public class UpdateProjectRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Descriptions { get; set; }
        public List<UserRequest> Users { get; set; }
    }
}
