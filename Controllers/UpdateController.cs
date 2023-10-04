using UpdateAPI.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UpdateAPI.Controllers
{

    
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateController : ControllerBase
    {

        private readonly StudentDataContext _dbContext;

        public UpdateController(StudentDataContext dbContext)
        {
            _dbContext = dbContext;
        }

//  -------------------- UPDATE METHOD  -------------------------

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateStudent(string id, Student updatedStudent)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingStudent = await _dbContext.Students.FirstOrDefaultAsync(s => s.StudentId == id);

                if (existingStudent == null)
                {
                    return NotFound("Student not found");
                }

                // Update the existing student properties with non-null values from updatedStudent
                existingStudent.Gender = updatedStudent.Gender ?? existingStudent.Gender;
                existingStudent.NationalIty = updatedStudent.NationalIty ?? existingStudent.NationalIty;
                existingStudent.PlaceofBirth = updatedStudent.PlaceofBirth ?? existingStudent.PlaceofBirth;
                existingStudent.StageId = updatedStudent.StageId ?? existingStudent.StageId;
                existingStudent.GradeId = updatedStudent.GradeId ?? existingStudent.GradeId;
                existingStudent.SectionId = updatedStudent.SectionId ?? existingStudent.SectionId;
                existingStudent.Topic = updatedStudent.Topic ?? existingStudent.Topic;
                existingStudent.Semester = updatedStudent.Semester ?? existingStudent.Semester;
                existingStudent.Relation = updatedStudent.Relation ?? existingStudent.Relation;
                existingStudent.Raisedhands = updatedStudent.Raisedhands ?? existingStudent.Raisedhands;
                existingStudent.VisItedResources = updatedStudent.VisItedResources ?? existingStudent.VisItedResources;
                existingStudent.AnnouncementsView = updatedStudent.AnnouncementsView ?? existingStudent.AnnouncementsView;
                existingStudent.Discussion = updatedStudent.Discussion ?? existingStudent.Discussion;
                existingStudent.ParentAnsweringSurvey = updatedStudent.ParentAnsweringSurvey ?? existingStudent.ParentAnsweringSurvey;
                existingStudent.ParentschoolSatisfaction = updatedStudent.ParentschoolSatisfaction ?? existingStudent.ParentschoolSatisfaction;
                existingStudent.StudentAbsenceDays = updatedStudent.StudentAbsenceDays ?? existingStudent.StudentAbsenceDays;
                existingStudent.StudentMarks = updatedStudent.StudentMarks ?? existingStudent.StudentMarks;
                existingStudent.Class = updatedStudent.Class ?? existingStudent.Class;

                _dbContext.Entry(existingStudent).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                return Ok(existingStudent);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                //give the valid message with status code
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
