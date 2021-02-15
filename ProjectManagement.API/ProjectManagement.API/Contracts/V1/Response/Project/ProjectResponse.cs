using ProjectManagement.API.Contracts.V1.Response.User;
using System;
using System.Collections.Generic;

namespace ProjectManagement.API.Contracts.V1.Response.Project
{
    public class ProjectResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool Deleted { get; set; }
        public List<UserResponse> Users { get; set; } = new List<UserResponse>();
    }
}
