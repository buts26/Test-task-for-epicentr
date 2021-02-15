using AutoMapper;
using ProjectManagement.BLL.Models.User.Request;
using ProjectManagement.BLL.Models.User.Response;
using ProjectManagement.BLL.Service.Interfaces;
using ProjectManagement.EntityFramework.Shared.Entities;
using ProjectManagement.EntityFramework.Shared.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.BLL.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IProjectRepository projectRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }


        public async Task<UserResult> AddAsync(UserRequest item)
        {
            var user = _mapper.Map<User>(item);
            if (user.Projects.Count > 0) //нагородив фігні
            {
                var projects = await _projectRepository.GetListAsync(x => user.Projects.Contains(x)); 
                user.Projects = projects;
            }
            var result = await _userRepository.AddAsync(user);
            return _mapper.Map<UserResult>(result);
        }

        public async Task<UserResult> UpdateAsync(UserRequest item)
        {
            var user = _mapper.Map<User>(item);
            if (user.Projects.Count > 0) //нагородив фігні
            {
                user.Projects = await _projectRepository.GetListAsync(x => user.Projects.Contains(x));
            }
            var result = await _userRepository.UpdateAsync(user);
            return _mapper.Map<UserResult>(result);
        }

        public async Task<UserResult> GetByIdAsync(string id)
        {
            var result = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserResult>(result);
        }

        public async Task<List<UserResult>> GetAllAsync()
        {
            var result = await _userRepository.GetListAsync();
            return _mapper.Map<List<UserResult>>(result);
        }

        public async Task DeleteAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<UserResult> GetUserByEmailAsync(string email)
        {
            var result = await _userRepository.GetByEmailAsync(email);
            return _mapper.Map<UserResult>(result);
        }

        public async Task<bool> IsExist(string email)
        {
            return await _userRepository.GetByEmailAsync(email) != null;
        }
    }
}
