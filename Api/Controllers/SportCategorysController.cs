using Application.Features.SportCategoryFeature.Commands.AddSportCategory;
using Application.Features.SportCategoryFeature.Commands.DeleteSportCategory;
using Application.Features.SportCategoryFeature.Commands.UpdateSportCategory;
using Application.Features.SportCategoryFeature.Queries.GetAllSportCategoryQueries;
using Application.Features.SportCategoryFeature.Queries.GetSportCategoryById;
using Application.Features.SportFeature.Commands.AddSport;
using Application.Features.SportFeature.Commands.UpdateSport;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin,User,SuperAdmin")]
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
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> AddSportCategory([FromForm] AddSportCategoryCommand addSportCategoryCommand)
        {
            try
            {
                if (addSportCategoryCommand == null)
                {
                    return BadRequest("Sport cannot be null.");
                }
                // Log to check if the image is being received
                if (addSportCategoryCommand.ImageUpload != null)
                {
                    Console.WriteLine($"Received Image: {addSportCategoryCommand.ImageUpload.FileName}");
                }
                else
                {
                    Console.WriteLine("No image uploaded.");
                }

                SportCategory addedSport = await _mediator.Send(addSportCategoryCommand);
                return Ok(addedSport);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An error occurred while adding the Sport. Details: {ex.Message}");
            }
        }


        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "SuperAdmin")]
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
         [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UpdateSportCategory([FromForm] UpdateSportCategoryCommand command)
        {
            try
            {
                if (command == null)
                {
                    return BadRequest("Command cannot be null.");
                }

                await _mediator.Send(command);
                return Ok("Sport Category Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Sport Category. Details: {ex.Message}");
            }
        }
        
        
        
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User,SuperAdmin")]
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
