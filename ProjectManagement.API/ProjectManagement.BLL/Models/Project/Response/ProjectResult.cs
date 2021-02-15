using ProjectManagement.EntityFramework.Shared.Entities;
using ProjectManagement.EntityFramework.Shared.Enums;
using System;
using System.Collections.Generic;
using ProjectManagement.BLL.Models.User.Response;

namespace ProjectManagement.BLL.Models.Project.Response
{
    public class ProjectResult
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool Deleted { get; set; }
        public List<UserResult> Users { get; set; } =new List<UserResult>();
    }
}
