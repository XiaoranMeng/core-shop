using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Errors;

namespace Web.Controllers
{
    public class TestController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("testauth")]
        public ActionResult<string> GetSecretText()
        {
            return "secret text";
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = _context.Products.Find(100);

            if (product is null)
            {
                return NotFound(new ResponseBody(404));
            }
             
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var entity = _context.Products.Find(100);
            var response = entity.ToString();
            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ResponseBody(400));
        }

        // Returns validation errors response as configured in service collection
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }
    }
}
