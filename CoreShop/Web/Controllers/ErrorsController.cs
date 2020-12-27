using Microsoft.AspNetCore.Mvc;
using Web.Errors;

namespace Web.Controllers
{
    [Route("errors/{code}")]
    public class ErrorsController : BaseController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ResponseBody(code));
        }
    }
}
