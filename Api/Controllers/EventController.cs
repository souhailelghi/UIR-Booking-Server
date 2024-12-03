using Application.Features.EventFeature.Commands.AddEvent;
using Application.Features.EventFeature.Commands.DeleteEvent;
using Application.Features.EventFeature.Commands.UpdateEvent;
using Application.Features.EventFeature.Queries.GetAllEventQueries;
using Application.Features.EventFeature.Queries.GetEventById;
using Application.Features.SportCategoryFeature.Commands.DeleteSportCategory;
using Application.Features.SportCategoryFeature.Commands.UpdateSportCategory;
using Application.Features.SportCategoryFeature.Queries.GetAllSportCategoryQueries;
using Application.Features.SportCategoryFeature.Queries.GetSportCategoryById;
using Application.Features.SportFeature.Commands.AddSport;
using Application.Features.SportFeature.Commands.DeleteSport;
using Application.Features.SportFeature.Commands.UpdateSport;
using Application.Features.SportFeature.Queries.GetAllSportByCategorieIdQuerie;
using Application.Features.SportFeature.Queries.GetAllSportsQuerie;
using Application.Features.SportFeature.Queries.GetSportById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("list")]
        // [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetEventsList()
        {
            try
            {
                List<Event> EventsList = await _mediator.Send(new GetAllEventQueries());
                return Ok(EventsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add")]
        //[Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> AddEvent([FromForm] AddEventCommand addEventCommand)
        {
            try
            {
                if (addEventCommand == null)
                {
                    return BadRequest("Event cannot be null.");
                }
                // Log to check if the image is being received
                if (addEventCommand.ImageUpload != null)
                {
                    Console.WriteLine($"Received Image: {addEventCommand.ImageUpload.FileName}");
                }
                else
                {
                    Console.WriteLine("No image uploaded.");
                }

                Event addedevent = await _mediator.Send(addEventCommand);
                return Ok(addedevent);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An error occurred while adding the Sport. Details: {ex.Message}");
            }
        }



        // d u g 
        [HttpDelete("delete/{id}")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteEventCommand(id));
                return Ok("Event deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Event. Details: {ex.Message}");
            }
        }


        [HttpPut("update")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UpdateSportCategory([FromForm] UpdateEventCommand command)
        {
            try
            {
                if (command == null)
                {
                    return BadRequest("Command cannot be null.");
                }

                await _mediator.Send(command);
                return Ok("Event Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Event. Details: {ex.Message}");
            }
        }



        [HttpGet("Get-EventById/{id}")]
        //[Authorize(Roles = "Admin,User,SuperAdmin")]
        public async Task<IActionResult> GetEventById(Guid id)
        {
            try
            {
                Event OneEvent = await _mediator.Send(new GetEventByIdQueries(id));
                return Ok(OneEvent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




    }
}
