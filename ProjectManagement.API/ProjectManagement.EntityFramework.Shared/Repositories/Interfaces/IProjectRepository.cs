using ProjectManagement.EntityFramework.Shared.Entities;
using System.Threading.Tasks;

namespace ProjectManagement.EntityFramework.Shared.Repositories.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<Project> GetByNameAsync(string projectName);
    }
}
