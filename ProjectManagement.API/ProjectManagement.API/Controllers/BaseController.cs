using Microsoft.AspNetCore.Mvc;
using ProjectManagement.API.Contracts.Common;

namespace ProjectManagement.API.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        public BaseController()
        {
        }

        public ActionResult PmResponse<T>(Result<T> data)
        {
            if (data.IsSuccess)
            {
                return Ok(new ResponseModel<T>(data));
            }
            else
            {
                return BadRequest(new ResponseModel<T>(data));
            }
        }
    }
}
