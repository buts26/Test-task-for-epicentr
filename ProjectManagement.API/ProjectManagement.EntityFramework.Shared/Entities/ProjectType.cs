using ProjectManagement.EntityFramework.Shared.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.EntityFramework.Shared.Entities
{
    public class ProjectType
    {
        [Key]
        public ProjectTypeEnum ProjectTypeId { get; set; }
        public string Name { get; set; }
        public List<Project> Projects { get; set; }
    }
}
