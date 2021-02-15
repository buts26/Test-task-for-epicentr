using ProjectManagement.BLL.Common;
using ProjectManagement.BLL.Models.User.Request;
using ProjectManagement.BLL.Models.User.Response;
using System.Threading.Tasks;

namespace ProjectManagement.BLL.Service.Interfaces
{
    public interface IUserService : IEntityService<UserRequest,UserResult>
    {
        /// <summary>
        /// Get User by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<UserResult> GetUserByEmailAsync(string email);

        /// <summary>
        /// Check if user exist by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> IsExist(string email);
    }
}
