using ProjectManagement.BLL.Models.User.Request;
using System.Collections.Generic;

namespace ProjectManagement.API.Configuration
{
    public class UserDataConfiguration
    {
        public List<UserRequest> Users { get; set; } = new List<UserRequest>(); 
    }
}
