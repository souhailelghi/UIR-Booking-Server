using Application.Features.SportFeature.Commands.AddSport;
using Application.Features.SportFeature.Queries.GetSportById;
using Application.Features.StudentFeature.Commands.AddStudent;
using Application.Features.StudentFeature.Queries.GetCheckCodeUirQuerie;
using Application.Features.StudentFeature.Queries.GetStudentByCodeUIRQuerie;
using Application.Features.StudentFeature.Queries.GetStudentByIdQuerie;
using Application.Features.StudentFeature.Queries.GetStudntByUserIdQuerie;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("checkCodeUIR/{codeUir}")]
        [Authorize(Roles = "Admin,User,SuperAdmin")]
        public async Task<IActionResult> CheckCodeUIR(string codeUir)
        {
            try
            {
                var exists = await _mediator.Send(new GetCheckCodeUirQuery(codeUir));
                return Ok(exists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred. Details: {ex.Message}");
            }
        }


        [HttpPost("add")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentCommand addStudentCommand)
        {
            try
            {
                if (addStudentCommand == null)
                {
                    return BadRequest("Student cannot be null.");
                }

                Student addedStudent = await _mediator.Send(addStudentCommand);
                return Ok(addedStudent);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An error occurred while adding the Student . Details: {ex.Message}");
            }
        }

        [HttpGet("GetStudentByUserId/{userId}")]
        [Authorize(Roles = "Admin,User,SuperAdmin")]
        public async Task<IActionResult> GetStudentByUserId(Guid userId)
        {
            try
            {
                var student = await _mediator.Send(new GetStudntByUserIdQuery(userId));
                return Ok(student);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the student. Details: {ex.Message}");
            }
        }


        [HttpGet("student/{id}")]
        [Authorize(Roles = "Admin,User,SuperAdmin")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            try
            {
                Student OneStudent = await _mediator.Send(new GetStudentByIdQuery(id));
                return Ok(OneStudent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("GetStudentByCodeUIR/{codeUIR}")]
         [Authorize(Roles = "Admin,User,SuperAdmin")]
        public async Task<IActionResult> GetStudentByCodeUIR(string codeUIR)
        {
            try
            {
                var student = await _mediator.Send(new GetStudentByCodeUIRQuery(codeUIR));
                return Ok(student);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the student. Details: {ex.Message}");
            }
        }
    }
}
