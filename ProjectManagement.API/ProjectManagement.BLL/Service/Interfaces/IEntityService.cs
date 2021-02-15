using ProjectManagement.BLL.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.BLL.Service.Interfaces
{
    public interface IEntityService<in TRequest,TResponse>: IService
    {
        Task<TResponse> AddAsync(TRequest item);
        Task<TResponse> UpdateAsync(TRequest item);
        Task<TResponse> GetByIdAsync(string id);
        Task<List<TResponse>> GetAllAsync();
        Task DeleteAsync(string item);
    }
}
