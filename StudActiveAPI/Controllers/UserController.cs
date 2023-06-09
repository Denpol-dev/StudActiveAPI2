using Microsoft.AspNetCore.Mvc;
using StudActiveAPI.Models;
using StudActiveAPI.Services;

namespace StudActiveAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private Context _context;
        public UserController(Context context) { _context = context; }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            return Ok(await model.LoginHash());
        }

        [HttpGet]
        public JsonResult GetUsers()
        {
            return new JsonResult(_context.Users.ToList());
        }
    }
}