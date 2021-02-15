using ProjectManagement.API.Configuration.Interfaces;

namespace ProjectManagement.API.Configuration
{
    public class RootConfiguration : IRootConfiguration
    {
        public UserDataConfiguration UserDataConfiguration { get; set; } = new UserDataConfiguration();
        public ProjectDataConfiguration ProjectDataConfiguration { get; set; } = new ProjectDataConfiguration();
    }
}