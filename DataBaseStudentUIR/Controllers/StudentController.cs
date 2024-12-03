using DataBaseStudentUIR.Models;
using DataBaseStudentUIR.Repositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataBaseStudentUIR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        [HttpPost("CheckEmailAndPassword")]
        public async Task<IActionResult> Check([FromBody] LoginRequest loginRequest)
        {
            var (isValid, codeUIR, firstName, lastName) = await _studentRepository.GetStudentDetailsAsync(loginRequest.Email, loginRequest.Password);

            if (isValid)
            {
                return Ok(new { CodeUIR = codeUIR, FirstName = firstName, LastName = lastName });
            }

            return Unauthorized(new { Message = "Invalid credentials" });
        }

        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }




        //// POST method to add a new student
        [HttpPost("add")]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            if (student == null || string.IsNullOrEmpty(student.Email) || string.IsNullOrEmpty(student.Password))
            {
                return BadRequest("Invalid student data. Email and Password are required.");
            }

            // Add the new student
            await _studentRepository.AddStudentAsync(student);

            return Ok("Student added successfully.");
        }

        // GET method to retrieve all students
        [HttpGet("all")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            return Ok(students);
        }


        [HttpDelete("deleteAll")]
        public async Task<IActionResult> DeleteAllStudents()
        {
            await _studentRepository.DeleteAllStudentsAsync();
            return Ok("All students have been deleted successfully.");
        }
    }
}
