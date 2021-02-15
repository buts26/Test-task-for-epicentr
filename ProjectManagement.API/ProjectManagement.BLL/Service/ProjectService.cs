using AutoMapper;
using ProjectManagement.BLL.Models.Project.Request;
using ProjectManagement.BLL.Models.Project.Response;
using ProjectManagement.BLL.Service.Interfaces;
using ProjectManagement.EntityFramework.Shared.Entities;
using ProjectManagement.EntityFramework.Shared.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.BLL.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectResult> AddAsync(ProjectRequest item)
        {
            var project = _mapper.Map<Project>(item);
            var result = await _projectRepository.AddAsync(project);
            return _mapper.Map<ProjectResult>(result);
        }

        public async Task<ProjectResult> UpdateAsync(ProjectRequest item)
        {
            var project = _mapper.Map<Project>(item);
            var result = await _projectRepository.UpdateAsync(project);
            return _mapper.Map<ProjectResult>(result);
        }

        public async Task<ProjectResult> GetByIdAsync(string id)
        {
            var result = await _projectRepository.GetByIdAsync(id);
            return _mapper.Map<ProjectResult>(result);
        }

        public async Task<List<ProjectResult>> GetAllAsync()
        {
            var result = await _projectRepository.GetListAsync();
            return _mapper.Map<List<ProjectResult>>(result);
        }

        public async Task DeleteAsync(string id)
        {
            await _projectRepository.DeleteAsync(id);
        }

        public async Task<ProjectResult> GetByNameAsync(string projectName)
        {
            var result = await _projectRepository.GetByNameAsync(projectName);
            return _mapper.Map<ProjectResult>(result);
        }

        public async Task<bool> IsExist(string projectName)
        {
            return await _projectRepository.GetByNameAsync(projectName) != null;
        }
    }
}