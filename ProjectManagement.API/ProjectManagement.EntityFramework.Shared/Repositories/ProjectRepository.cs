using Microsoft.EntityFrameworkCore;
using ProjectManagement.EntityFramework.Shared.DbContexts;
using ProjectManagement.EntityFramework.Shared.Entities;
using ProjectManagement.EntityFramework.Shared.Repositories.Interfaces;
using System.Threading.Tasks;

namespace ProjectManagement.EntityFramework.Shared.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext db) : base(db)
        {
        }

        public override async Task<Project> GetByIdAsync(string id)
        {
            return await _dbSet.Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Project> GetByNameAsync(string projectName)
        {
            return _dbSet.Include(x => x.Users).FirstOrDefaultAsync(u => u.Name == projectName);
        }
    }
}