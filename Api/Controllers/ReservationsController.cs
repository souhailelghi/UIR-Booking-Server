using Application.Features.ReservationFeature.Commands.AddReservation;
using Application.Features.ReservationFeature.Commands.DeleteAllReservations;
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

                // Attempt to create the reservation
                string reservationResult = await _mediator.Send(addReservationCommand);

                if (reservationResult == "Reservation successful")
                {
                    return Ok(new { message = reservationResult });
                }
                else
                {
                    return Conflict(new { error = reservationResult }); // Conflict message from the service
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the reservation. Details: {ex.Message}");
            }
        }

        [HttpDelete("deleteAll")]
        public async Task<IActionResult> DeleteAllReservations()
        {
            try
            {
                await _mediator.Send(new DeleteAllReservationsCommand());
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log exception details here
                return StatusCode(500, $"An error occurred while deleting Reservations. Details: {ex.Message}");
            }
        }


    }
}
