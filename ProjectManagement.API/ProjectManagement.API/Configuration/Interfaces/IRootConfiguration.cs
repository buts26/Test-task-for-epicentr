namespace ProjectManagement.API.Configuration.Interfaces
{
    public interface IRootConfiguration
    {
        UserDataConfiguration UserDataConfiguration { get; }
        ProjectDataConfiguration ProjectDataConfiguration { get; }
    }
}
