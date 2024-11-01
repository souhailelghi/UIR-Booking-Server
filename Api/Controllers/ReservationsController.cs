using Application.Features.ReservationFeature.Commands.AddReservation;
using Application.Features.ReservationFeature.Commands.DeleteAllReservations;
using Application.Features.ReservationFeature.Queries.GetAllReservationQuerie;
using Application.Features.ReservationFeature.Queries.GetReservationByIdQuerie;
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
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

      


        [HttpPost("AddReservations")]
        public async Task<ActionResult<string>> AddReservations([FromBody] AddReservationCommand addReservationCommand)
        {
            if (addReservationCommand == null)
            {
                return BadRequest("Reservation command cannot be null.");
            }

            // Send the command to the handler
            var result = await _mediator.Send(addReservationCommand);

            // Return specific error message if booking fails
            if (result != "Reservation successfully created.")
            {
                return BadRequest(result);
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



        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(Guid id)
        {
            try
            {
                Reservation reservationById = await _mediator.Send(new GetReservationByIdQuery(id));
                return Ok(reservationById);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("list")]
        //[Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetReservationsList()
        {
            try
            {
                List<Reservation> ReservationList = await _mediator.Send(new GetAllReservationQuery());
                return Ok(ReservationList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



    }
}
