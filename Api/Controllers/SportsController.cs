using Application.Features.SportCategoryFeature.Commands.AddSportCategory;
using Application.Features.SportCategoryFeature.Commands.DeleteSportCategory;
using Application.Features.SportCategoryFeature.Commands.UpdateSportCategory;
using Application.Features.SportCategoryFeature.Queries.GetAllSportCategoryQueries;
using Application.Features.SportCategoryFeature.Queries.GetSportCategoryById;
using Application.Features.SportFeature.Commands.AddSport;
using Application.Features.SportFeature.Commands.DeleteSport;
using Application.Features.SportFeature.Commands.UpdateSport;
using Application.Features.SportFeature.Queries.GetAllSportsQuerie;
using Application.Features.SportFeature.Queries.GetSportById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetSportsList()
        {
            try
            {
                List<Sport> SportsList = await _mediator.Send(new GetAllSportQuerie());
                return Ok(SportsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> AddSport([FromBody] AddSportCommand addSportCommand)
        {
            try
            {
                if (addSportCommand == null)
                {
                    return BadRequest("Sport cannot be null.");
                }

                Sport addedSport = await _mediator.Send(addSportCommand);
                return Ok(addedSport);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An error occurred while adding the Sport. Details: {ex.Message}");
            }
        }





        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSport(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteSportCommand(id));
                return Ok("Sport deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Sport. Details: {ex.Message}");
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateSport([FromBody] UpdateSportCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok("Sport Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Sport. Details: {ex.Message}");
            }
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetSportById(Guid id)
        {
            try
            {
                Sport OneSport = await _mediator.Send(new GetSportByIdQuery(id));
                return Ok(OneSport);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
