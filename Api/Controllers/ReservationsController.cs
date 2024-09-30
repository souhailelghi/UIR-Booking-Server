using Application.Features.ReservationFeature.Commands.AddReservation;
using MediatR;
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


        [HttpPost("AddReservations")]
        public async Task<ActionResult> AddReservations([FromBody] AddReservationCommand addReservationCommand)
        {
            try
            {
                if (addReservationCommand.StudentIdList == null || addReservationCommand.StudentIdList.Count == 0)
                {
                    return BadRequest(new { error = "StudentIdList cannot be empty." });
                }

                if (addReservationCommand.SportId == Guid.Empty)
                {
                    return BadRequest(new { error = "Invalid sport ID." });
                }

                bool isReservationSuccessful = await _mediator.Send(addReservationCommand);
                Console.WriteLine($"Reservation Success: {isReservationSuccessful}");

                if (isReservationSuccessful)
                {
                    return Ok(new { message = "Reservation successful!" });
                }
                else
                {
                    return Conflict(new { error = "Reservation could not be created. It may conflict with existing reservations." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the reservation. Details: {ex.Message}");
            }
        }


    }
}
