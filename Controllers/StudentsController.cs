using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndiaWalks.API.Controllers
{
    // https://localhost:portnumber/api/Students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GET: https://localhost:portnumber/api/Students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[] {"Faizan","Harish","Sohal","Karthik","Venky" }; 

            return Ok(studentNames);
        }
    }
}
