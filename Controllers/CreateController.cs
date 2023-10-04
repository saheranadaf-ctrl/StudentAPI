using CreateAPI.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CreateAPI.Controllers
{

    
    [ApiController]
    [Route("api/[controller]")]
    public class CreateController : ControllerBase
    {

        private readonly StudentDataContext _dbContext;

        public CreateController(StudentDataContext dbContext)
        {
            _dbContext = dbContext;
        }

// ------------------------ CREATE METHOD -------------------------------

        [HttpPost("Create")]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                
                // Add any additional data validation logic here

                _dbContext.Students.Add(student);
                 await _dbContext.SaveChangesAsync();

                return CreatedAtAction("GetStudentById", new { id = student.StudentId }, student);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                //give the valid message with status code
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpGet("{id}")]
        public IActionResult GetStudentById(string id)
        {
            try
            {

                var student = _dbContext.Students.FirstOrDefault(o=>o.StudentId == id);

                if (student == null)
                {
                    return NotFound("Student not found"); // Return 404 Not Found if the student with the given id is not found
                }

                return Ok(student); // Return 200 OK with the student data if found
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
