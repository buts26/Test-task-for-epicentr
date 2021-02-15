using AutoMapper;
using ProjectManagement.API.Contracts.V1.Request.Project;
using ProjectManagement.API.Contracts.V1.Response.Project;
using ProjectManagement.BLL.Models.Project.Request;
using ProjectManagement.BLL.Models.Project.Response;
using ProjectManagement.EntityFramework.Shared.Entities;

namespace ProjectManagement.API.MapProfile
{
    public class ProjectMapProfile:Profile

    {
        public ProjectMapProfile()
        {
            CreateMap<Project, ProjectResult>().ReverseMap();
            CreateMap<Project, ProjectRequest>().ReverseMap();
            CreateMap<ProjectResponse, ProjectResult>().ReverseMap();
            CreateMap<CreateProjectRequest, ProjectRequest>().ReverseMap();
            CreateMap<ProjectRequest, ProjectRequest>().ReverseMap();
        }
    }
}
