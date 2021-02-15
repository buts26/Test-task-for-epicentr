using ProjectManagement.EntityFramework.Shared.Entities;
using System.Threading.Tasks;

namespace ProjectManagement.EntityFramework.Shared.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
