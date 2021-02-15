using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.API.Contracts.Common;
using ProjectManagement.API.Contracts.V1;
using ProjectManagement.API.Contracts.V1.Request.User;
using ProjectManagement.API.Contracts.V1.Response.User;
using ProjectManagement.BLL.Models.User.Request;
using ProjectManagement.BLL.Models.User.Response;
using ProjectManagement.BLL.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.API.Controllers.V1
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all or one user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.User.Get)]
        public async Task<IActionResult> Get(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var serviceResult = await _userService.GetByIdAsync(id);
                var user = _mapper.Map<UserResponse>(serviceResult);
                return PmResponse(Result.Success(user));
            }
            else
            {
                var serviceResult = await _userService.GetAllAsync();
                var users = _mapper.Map<List<UserResponse>>(serviceResult);
                return PmResponse(Result.Success(users));
            }
        }

        [HttpPost(ApiRoutes.User.Create)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] CreateUserRequest createUserRequest)
        {
            if (!ModelState.IsValid) return ValidationProblem();
            var createRequest = _mapper.Map<CreateUserRequest, UserRequest>(createUserRequest);
            var serviceResult = await _userService.AddAsync(createRequest);
            var user = _mapper.Map<UserResult, UserResponse>(serviceResult);
            return PmResponse(Result.Success(user));
        }

        [HttpPut(ApiRoutes.User.Update)]
        public async Task<IActionResult> Put([FromBody] UpdateUserRequest updateUserRequest)
        {
            if (await _userService.IsExist(updateUserRequest.Email))
            {
                var userRequest = _mapper.Map<UserRequest>(updateUserRequest);
                var serviceResult = await _userService.UpdateAsync(userRequest);
                var result = _mapper.Map<UserResponse>(serviceResult);
                return PmResponse(Result.Success(result));
            }
            return NotFound();
        }

        [HttpDelete(ApiRoutes.User.Delete)]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteAsync(id);
            return Ok();
        }
    }
}
