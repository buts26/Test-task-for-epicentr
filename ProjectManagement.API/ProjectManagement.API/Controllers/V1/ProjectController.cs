using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.API.Contracts.Common;
using ProjectManagement.API.Contracts.V1;
using ProjectManagement.API.Contracts.V1.Request.Project;
using ProjectManagement.API.Contracts.V1.Response.Project;
using ProjectManagement.BLL.Models.Project.Request;
using ProjectManagement.BLL.Models.Project.Response;
using ProjectManagement.BLL.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.API.Controllers.V1
{
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        public ProjectController(IProjectService projectService,
            IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all or one user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Project.Get)]
        public async Task<IActionResult> Get(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var serviceResult = await _projectService.GetByIdAsync(id);
                var project = _mapper.Map<ProjectResponse>(serviceResult);
                return PmResponse(Result.Success(project));
            }
            else
            {
                var serviceResult = await _projectService.GetAllAsync();
                var projects = _mapper.Map<List<ProjectResponse>>(serviceResult);
                return PmResponse(Result.Success(projects));
            }
        }

        [HttpPost(ApiRoutes.Project.Create)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequest createProjectRequest)
        {
            if (!ModelState.IsValid) return ValidationProblem();
            var createRequest = _mapper.Map<CreateProjectRequest,ProjectRequest>(createProjectRequest);
            var serviceResult = await _projectService.AddAsync(createRequest);
            var project = _mapper.Map<ProjectResult,ProjectResponse>(serviceResult);
            return PmResponse(Result.Success(project));
        }
    }
}
