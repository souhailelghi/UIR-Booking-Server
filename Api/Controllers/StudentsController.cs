using Application.Features.SportFeature.Commands.AddSport;
using Application.Features.StudentFeature.Commands.AddStudent;
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


        [HttpPost("add")]
        //[Authorize(Roles = "Admin,User")]
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
    }
}
