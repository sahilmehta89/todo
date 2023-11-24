using Microsoft.AspNetCore.Mvc;
using Todo.Utilities.Common;

namespace Todo.API.Controllers
{
    public class BaseController : ControllerBase
    {
        [NonAction]
        public ObjectResult GetStatusCodeWithProblemDetails(int statusCode, string title = null)
        {
            switch (statusCode)
            {
                case StatusCodes.Status400BadRequest:
                    return BadRequest(CommonFunctions.GetProblemDetails(statusCode, title));
                default:
                    return StatusCode(statusCode, CommonFunctions.GetProblemDetails(statusCode, title));
            }
        }
    }

}
