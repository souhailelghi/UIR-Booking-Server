
using Application.Features.ReservationFeature.Commands.AddReservation;
using Application.Features.ReservationFeature.Commands.DeleteAllReservations;

using Application.Features.ReservationFeature.Queries.CheckReservationAccessRequestQuerie;
using Application.Features.ReservationFeature.Queries.CountTimeForReservation;
using Application.Features.ReservationFeature.Queries.GetAllReservationQuerie;
using Application.Features.ReservationFeature.Queries.GetReservationByIdQuerie;
using Application.Features.ReservationFeature.Queries.GetReservationsByCategorieIdQuerie;
using Application.Features.ReservationFeature.Queries.GetReservationsByCategoryIdAndStudentIdQuerie;
using Application.Features.ReservationFeature.Queries.GetReservationsByStudentIdQuerie;
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

        //// Endpoint to check if a user or team can make a reservation
        //[HttpPost("check-reservation-time")]
        //public async Task<IActionResult> CheckReservationTime([FromBody] CountTimeForReservationQuery request)
        //{
        //    // Send the query to the handler and get the result
        //    var result = await _mediator.Send(request);

        //    if (result.StartsWith("You don't have permission"))
        //    {
        //        // If the user needs to wait, return a message with the remaining time
        //        return Ok(result); // The message contains the remaining time
        //    }

        //    return Ok("You can make a reservation."); // If no restrictions, permission is granted
        //}
        [HttpPost("check-reservation-time")]
        public async Task<IActionResult> CheckReservationTime([FromBody] CountTimeForReservationQuery request)
        {
            var result = await _mediator.Send(request);

            if (result.StartsWith("You don't have permission"))
            {
                return Ok(result); // Return the remaining time message
            }

            return Ok(result); // Return a success message
        }



        // Endpoint to check if a user or team can make a reservation
        [HttpPost("check-access")]
        public async Task<IActionResult> CheckReservationAccess([FromBody] CheckReservationAccessRequest request)
        {
            var result = await _mediator.Send(request);

            if (result==false)
            {
                return Ok(false); // If there are any errors, return BadRequest
            }

            return Ok(true); // If access is allowed, return success
        }


        [HttpPost("AddReservations")]
        //[Authorize(Roles = "Admin,User,SuperAdmin")]
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



        [HttpGet("ByCategoryAndStudentId/{sportCategoryId}/{codeUIR}")]
        [Authorize(Roles = "Admin,User,SuperAdmin")]
        public async Task<IActionResult> GetReservationsByCategoryAndStudentId(Guid sportCategoryId, string codeUIR)
        {
            try
            {
                var reservations = await _mediator.Send(new GetReservationsByCategoryIdAndStudentIdQuery(sportCategoryId, codeUIR));
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("BySportCategoryId/{sportCategoryId}")]
        [Authorize(Roles = "Admin,User,SuperAdmin")]
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




        [HttpGet("byStudent/{codeUIR}")]
        [Authorize(Roles = "Admin,User,SuperAdmin")]
        public async Task<IActionResult> GetReservationsByStudentId(string codeUIR)
        {
            try
            {
                List<Reservation> reservations = await _mediator.Send(new GetReservationsByStudentIdQuery(codeUIR));
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }





        [HttpDelete("deleteAll")]
        [Authorize(Roles = "SuperAdmin")]
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
        [Authorize(Roles = "Admin,User,SuperAdmin")]
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
        [Authorize(Roles = "Admin,User,SuperAdmin")]
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