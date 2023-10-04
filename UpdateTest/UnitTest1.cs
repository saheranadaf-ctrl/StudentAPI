using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UpdateAPI.Controllers;
using UpdateAPI.DataAccess;
using Xunit;

using Xunit;

namespace UpdateTest;

public class UnitTest1
{

        [Fact]
        public async Task UpdateStudent_ValidId_ReturnsOkResult()
        {
            // Arrange
            var dbContextMock = new Mock<StudentDataContext>();
            var controller = new UpdateController(dbContextMock.Object);
            var existingStudentId = "STDN123"; // Replace with an existing student ID
            var updatedStudent = new Student { StudentId = existingStudentId, Gender = "F" }; // Updated student with new gender

            dbContextMock.Setup(x => x.Students.FindAsync(existingStudentId))
                        .ReturnsAsync(new Student { StudentId = existingStudentId });

            // Act
            var result = await controller.UpdateStudent(existingStudentId, updatedStudent);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var updatedStudentResult = Assert.IsType<Student>(okResult.Value);
            Assert.Equal(existingStudentId, updatedStudentResult.StudentId);
            Assert.Equal(updatedStudent.Gender, updatedStudentResult.Gender);
        }

        [Fact]
        public async Task UpdateStudent_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var dbContextMock = new Mock<StudentDataContext>();
            var controller = new UpdateController(dbContextMock.Object);
            var nonExistingStudentId = "NONEXIST123"; // Replace with a non-existing student ID
            var updatedStudent = new Student { StudentId = nonExistingStudentId, Gender = "F" };

            dbContextMock.Setup(x => x.Students.FindAsync(nonExistingStudentId))
                        .ReturnsAsync((Student)null);

            // Act
            var result = await controller.UpdateStudent(nonExistingStudentId, updatedStudent);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateStudent_InvalidModelState_ReturnsBadRequestResult()
        {
            // Arrange
            var dbContextMock = new Mock<StudentDataContext>();
            var controller = new UpdateController(dbContextMock.Object);
            var existingStudentId = "STDN123"; // Replace with an existing student ID
            var updatedStudent = new Student { StudentId = existingStudentId, Gender = null }; // Invalid model state

            // ModelState is invalid because 'Gender' is null

            // Act
            var result = await controller.UpdateStudent(existingStudentId, updatedStudent);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
}