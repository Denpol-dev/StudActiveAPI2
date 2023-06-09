using Microsoft.AspNetCore.Mvc;
using StudActiveAPI.Models;

namespace StudActiveAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {

        [HttpPut("changepass")]
        public async Task<IActionResult> ChangePass(string plainText, Guid userId)
        {
            return Ok(await StudentsActiveModel.ChangePass(plainText, userId));
        }
    }
}
