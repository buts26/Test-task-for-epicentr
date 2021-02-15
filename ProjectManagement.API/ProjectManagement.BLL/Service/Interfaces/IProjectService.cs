using ProjectManagement.BLL.Models.Project.Request;
using ProjectManagement.BLL.Models.Project.Response;
using System.Threading.Tasks;

namespace ProjectManagement.BLL.Service.Interfaces
{
    public interface IProjectService :IEntityService<ProjectRequest,ProjectResult>
    {
        /// <summary>
        /// Get Project by project name
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        Task<ProjectResult> GetByNameAsync(string projectName);

        /// <summary>
        /// Check if project exist by name
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        Task<bool> IsExist(string projectName);
    }
}
