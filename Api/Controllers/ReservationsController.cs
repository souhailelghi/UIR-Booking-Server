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




        //[HttpPost("AddReservations")]
        //public async Task<ActionResult<Reservation>> AddReservations([FromBody] AddReservationCommand addReservationCommand)
        //{
        //    // Validate the list of student IDs
        //    if (addReservationCommand.StudentIdList == null || addReservationCommand.StudentIdList.Count == 0)
        //    {
        //        return BadRequest(new { error = "StudentIdList cannot be empty." });
        //    }

        //    // Validate the sport ID
        //    if (addReservationCommand.SportId == Guid.Empty)
        //    {
        //        return BadRequest(new { error = "Invalid sport ID." });
        //    }

        //    try
        //    {
        //        // Use MediatR to send the command
        //        Reservation addedReservation = await _mediator.Send(true);

        //        // If the reservation is successfully added, return OK with the reservation details
        //        return Ok(new { message = "Reservation successful!", reservation = addedReservation });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Return a 500 error if something goes wrong
        //        return StatusCode(500, $"An error occurred while adding the reservation. Details: {ex.Message}");
        //    }
        //}

        [HttpPost("AddReservations")]
        public async Task<ActionResult> AddReservations([FromBody] AddReservationCommand addReservationCommand)
        {
            // Validate the list of student IDs
            if (addReservationCommand.StudentIdList == null || addReservationCommand.StudentIdList.Count == 0)
            {
                return BadRequest(new { error = "StudentIdList cannot be empty." });
            }

            // Validate the sport ID
            if (addReservationCommand.SportId == Guid.Empty)
            {
                return BadRequest(new { error = "Invalid sport ID." });
            }

            try
            {
                // Use MediatR to send the command
                bool isReservationSuccessful = await _mediator.Send(addReservationCommand);

                if (isReservationSuccessful)
                {
                    // If the reservation is successfully added, return OK with a success message
                    return Ok(new { message = "Reservation successful!" });
                }
                else
                {
                    // If the reservation failed, return a conflict response
                    return Conflict(new { error = "Reservation could not be created. It may conflict with existing reservations." });
                }
            }
            catch (Exception ex)
            {
                // Return a 500 error if something goes wrong
                return StatusCode(500, $"An error occurred while adding the reservation. Details: {ex.Message}");
            }
        }


    }
}
