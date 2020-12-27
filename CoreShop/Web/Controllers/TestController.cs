using Infrastructure.Data;
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

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            return _context.Products.Find(100) is null 
                ? NotFound(new ResponseBody(404)) 
                : Ok();
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
