using Microsoft.AspNetCore.Mvc;
using StudActiveAPI.Models;

namespace StudActiveAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudActiveController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetStudentActive([FromQuery] Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest("User Id cannot be Guid Empty");
            }
            return Ok(await StudentsActiveModel.GetStudentActive(userId));
        }

        [HttpPut("changestudentactive")]
        public async Task<IActionResult> ChangeStudentActive(RegistrationStudActiveModel student)
        {
            return Ok(await student.ChangeStudentActive());
        }
    }
}
