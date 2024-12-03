using Application.Features.EventFeature.Commands.AddEvent;
using Application.Features.EventFeature.Queries.GetAllEventQueries;
using Application.Features.SportCategoryFeature.Queries.GetAllSportCategoryQueries;
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
    }
}
