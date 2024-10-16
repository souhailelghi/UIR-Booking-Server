using Application.Features.ReservationFeature.Commands.AddReservation;
using Application.Features.ReservationFeature.Commands.DeleteAllReservations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<string>> AddReservations([FromBody] AddReservationCommand addReservationCommand)
        {
            if (addReservationCommand == null)
            {
                return BadRequest("Reservation command cannot be null.");
            }

            // Handle the command using MediatR
            var result = await _mediator.Send(addReservationCommand);

            // Check the result (You can customize this based on your needs)
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest("Failed to add reservation.");
            }

            return CreatedAtAction(nameof(AddReservations), new { id = result }, result);
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
