using Application.Features.SportCategoryFeature.Commands.AddSportCategory;
using Application.Features.SportCategoryFeature.Commands.DeleteSportCategory;
using Application.Features.SportCategoryFeature.Commands.UpdateSportCategory;
using Application.Features.SportCategoryFeature.Queries.GetAllSportCategoryQueries;
using Application.Features.SportCategoryFeature.Queries.GetSportCategoryById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportCategorysController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SportCategorysController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetSportCategorysList()
        {
            try
            {
                List<SportCategory> SportCategorysList = await _mediator.Send(new GetAllSportCategoryQuerie());
                return Ok(SportCategorysList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddSportCategory([FromBody] AddSportCategoryCommand addSportCategoryCommand)
        {
            try
            {
                if (addSportCategoryCommand == null)
                {
                    return BadRequest("SportCategory cannot be null.");
                }

                SportCategory addedSportCategory = await _mediator.Send(addSportCategoryCommand);
                return Ok(addedSportCategory);
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, $"An error occurred while adding the SportCategory. Details: {ex.Message}");
            }
        }



        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSportCategory(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteSportCategoryCommand(id));
                return Ok("SportCategory deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the SportCategory. Details: {ex.Message}");
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateSportCategory([FromBody] UpdateSportCategoryCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok("SportCategory Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the SportCategory. Details: {ex.Message}");
            }
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetSportCategoryById(Guid id)
        {
            try
            {
                SportCategory OneSportCategory = await _mediator.Send(new GetSportCategoryByIdQueries(id));
                return Ok(OneSportCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
