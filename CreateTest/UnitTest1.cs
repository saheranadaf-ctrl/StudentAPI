using CreateAPI.Controllers;
using CreateAPI.DataAccess;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.InMemory;

namespace CreateTest;

public class UnitTest1
{


        [Fact]
        public async Task CreateStudent_ValidModel_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StudentDataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new StudentDataContext(options))
            {
                var controller = new CreateController(dbContext);
                var student = new Student { StudentId = "STDN00481", Gender = "M" };

                // Act
                var result = await controller.CreateStudent(student);

                // Assert
                var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
                var createdStudent = Assert.IsType<Student>(createdAtActionResult.Value);
                Assert.Equal(student.StudentId, createdStudent.StudentId);
            }
        }

        [Fact]
        public async Task CreateStudent_InvalidModel_ReturnsBadRequestResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StudentDataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new StudentDataContext(options))
            {
                var controller = new CreateController(dbContext);
                var student = new Student { StudentId = null, Gender = "M" }; // Invalid model

                // Act
                var result = await controller.CreateStudent(student);

                // Assert
                Assert.IsType<BadRequestObjectResult>(result.Result);
            }
        }
}