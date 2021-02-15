using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.EntityFramework.Shared.Entities
{
    public class User
    {
        public User()
        {
        }

        public User(string userName, string firstName, string lastName) : this()
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
        }

        [Key] 
        public string Id { get; set; }

        [Required] 
        public string UserName { get; set; }

        [Required] 
        public string FirstName { get; set; }

        [Required] 
        public string LastName { get; set; }

        [Required] 
        public string Email { get; set; }

        public List<Project> Projects { get; set; } = new List<Project>();
    }
}