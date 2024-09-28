using Application.Features.ReservationFeature.Commands.AddReservation;
using Application.Features.StudentFeature.Commands.AddStudent;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddStudent([FromBody] AddReservationCommand addReservationCommand)
        {
            try
            {
                if (addReservationCommand == null)
                {
                    return BadRequest("Reservation  cannot be null.");
                }

                Reservation addedReservation = await _mediator.Send(addReservationCommand);
                return Ok(addedReservation);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An error occurred while adding the Reservation  . Details: {ex.Message}");
            }
        }
    }
}
