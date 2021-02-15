using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProjectManagement.EntityFramework.Shared.Enums;

namespace ProjectManagement.EntityFramework.Shared.Entities
{
    public class Project 
    {
        public Project()
        {
        }

        public Project(string name) : this()
        {
            Name = name;
        }

        [Key]
        public string Id { get; set; }

        [Required] 
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool Deleted { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}