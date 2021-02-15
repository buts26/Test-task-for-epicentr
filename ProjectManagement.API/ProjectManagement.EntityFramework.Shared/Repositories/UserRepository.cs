using Microsoft.EntityFrameworkCore;
using ProjectManagement.EntityFramework.Shared.DbContexts;
using ProjectManagement.EntityFramework.Shared.Entities;
using ProjectManagement.EntityFramework.Shared.Repositories.Interfaces;
using System.Threading.Tasks;

namespace ProjectManagement.EntityFramework.Shared.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext db) : base(db)
        {
        }

        public override Task<User> GetByIdAsync(string id)
        {
            return _dbSet.Include(x => x.Projects).FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<User> GetByEmailAsync(string email)
        {
            return _dbSet.Include(x => x.Projects).FirstOrDefaultAsync(u => u.Email == email);
        }

    }
}