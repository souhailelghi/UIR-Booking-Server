
using Application.Features.ReservationFeature.Commands.AddReservation;
using Application.Features.ReservationFeature.Commands.DeleteAllReservations;
using Application.Features.ReservationFeature.Queries.GetAllReservationQuerie;
using Application.Features.ReservationFeature.Queries.GetReservationByIdQuerie;
using Application.Features.ReservationFeature.Queries.GetReservationsByCategorieIdQuerie;
using Application.Features.ReservationFeature.Queries.GetReservationsByCategoryIdAndStudentIdQuerie;
using Application.Features.ReservationFeature.Queries.GetReservationsByStudentIdQuerie;
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

        [HttpGet("ByCategoryAndStudentId/{sportCategoryId}/{studentId}")]
        public async Task<IActionResult> GetReservationsByCategoryAndStudentId(Guid sportCategoryId, Guid studentId)
        {
            try
            {
                var reservations = await _mediator.Send(new GetReservationsByCategoryIdAndStudentIdQuery(sportCategoryId, studentId));
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("BySportCategoryId/{sportCategoryId}")]
        //[Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetReservationsBySportCategoryId(Guid sportCategoryId)
        {
            try
            {
                List<Reservation> reservations = await _mediator.Send(new GetReservationsByCategorieIdQuery(sportCategoryId));
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [HttpGet("byStudent/{studentId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetReservationsByStudentId(Guid studentId)
        {
            try
            {
                List<Reservation> reservations = await _mediator.Send(new GetReservationsByStudentIdQuery(studentId));
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost("AddReservations")]
        //[Authorize(Roles = "Admin,User")]
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
        [Authorize(Roles = "Admin,User")]
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
        [Authorize(Roles = "Admin,User")]
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
        [Authorize(Roles = "Admin,User")]
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
