using AutoMapper;
using ProjectManagement.API.Contracts.V1.Request.User;
using ProjectManagement.API.Contracts.V1.Response.User;
using ProjectManagement.BLL.Models.User.Request;
using ProjectManagement.BLL.Models.User.Response;
using ProjectManagement.EntityFramework.Shared.Entities;

namespace ProjectManagement.API.MapProfile
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<User, UserResult>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();
            CreateMap<User, UserRequest>().ReverseMap();
            CreateMap<UserResponse, UserResult>().ReverseMap();
            CreateMap<CreateUserRequest, UserRequest>().ReverseMap();
            CreateMap<UpdateUserRequest, UserRequest>().ReverseMap();
        }
    }
}
