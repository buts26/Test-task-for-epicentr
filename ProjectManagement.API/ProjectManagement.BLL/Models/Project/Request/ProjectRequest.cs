using ProjectManagement.BLL.Models.User.Request;
using ProjectManagement.EntityFramework.Shared.Entities;
using ProjectManagement.EntityFramework.Shared.Enums;
using System;
using System.Collections.Generic;

namespace ProjectManagement.BLL.Models.Project.Request
{
    public class ProjectRequest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool Deleted { get; set; }
        public List<UserRequest> Users { get; set; } = new List<UserRequest>();
    }
}
