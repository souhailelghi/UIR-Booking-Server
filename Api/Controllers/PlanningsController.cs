using Application.Features.PlanningFeature.Commands.AddPlanning;
using Application.Features.PlanningFeature.Commands.DeletePlanning;
using Application.Features.PlanningFeature.Commands.UpdatePlanning;
using Application.Features.PlanningFeature.Queries.GetAllPlanningQuerie;
using Application.Features.PlanningFeature.Queries.GetAllPlanningsBySportIdQuerie;
//using Application.Features.PlanningFeature.Queries.GetAllPlanningsGroupedBySportQuerie;
using Application.Features.PlanningFeature.Queries.GetAvailablePlanningQuerie;
using Application.Features.PlanningFeature.Queries.GetAvailableTimeRangesByReferenceSportAndDayQuerie;
using Application.Features.PlanningFeature.Queries.GetAvailableTimeRangesBySportAndDayQuerie;
using Application.Features.PlanningFeature.Queries.GetAvailableTimeRangesBySportQuerie;
using Application.Features.PlanningFeature.Queries.GetPlanningByIdQuerie;
using Application.Features.SportCategoryFeature.Commands.UpdateSportCategory;
using Application.Features.SportFeature.Commands.DeleteSport;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanningsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlanningsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-by-sport/{sportId}")]
        public async Task<IActionResult> GetAllPlanningsBySportId(Guid sportId)
        {
            try
            {
                var plannings = await _mediator.Send(new GetAllPlanningsBySportIdQuery(sportId));
                return Ok(plannings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("get-timeRanges-by-referenceSport-and-day/{referenceSport}/{day}")]
        // [Authorize(Roles = "Admin,User")] // Add authorization if needed
        public async Task<IActionResult> GetAvailableTimeRangesByReferenceSportAndDay(int referenceSport, DayOfWeekEnum day)
        {
            try
            {
                var availableTimeRanges = await _mediator.Send(new GetAvailableTimeRangesByReferenceSportAndDayQuery(referenceSport, day));

                if (availableTimeRanges == null || !availableTimeRanges.Any())
                {
                    return NotFound("No time ranges found for the specified reference sport and day.");
                }

                return Ok(availableTimeRanges);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("get-timeRanges-by-sport-and-day-not-reserved/{sportId}/{day}")]
        //[Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAvailableTimeRangesBySportAndDay(Guid sportId, DayOfWeekEnum day)
        {
            try
            {
                var availableTimeRanges = await _mediator.Send(new GetAvailableTimeRangesBySportAndDayQuery(sportId, day));

                if (availableTimeRanges == null || !availableTimeRanges.Any())
                {
                    return NotFound("No time ranges found for the specified sport and day.");
                }

                return Ok(availableTimeRanges);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("list")]
        public async Task<IActionResult> GetPlanningsList()
        {
            try
            {
                List<Planning> PlanningsList = await _mediator.Send(new GetAllPlanningQuery());
                return Ok(PlanningsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("get-AllTimeRanges")]
        public async Task<IActionResult> GetAvailableTimeRanges()
        {
            try
            {
                var availableTimeRanges = await _mediator.Send(new GetAvailableTimeRangesQuery());
                return Ok(availableTimeRanges);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-time-ranges-by-sport-not-reserved/{sportId}")]
        public async Task<IActionResult> GetAvailableTimeRangesBySport(Guid sportId)
        {
            try
            {
                var availableTimeRanges = await _mediator.Send(new GetAvailableTimeRangesBySportQuery(sportId));
                return Ok(availableTimeRanges);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add-planning")]
        public async Task<IActionResult> AddPlanning([FromBody] PlanningDto planningDto)
        {
            try
            {
                var result = await _mediator.Send(new AddPlanningCommand(planningDto));
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdatePlanning([FromBody] UpdatePlanningCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok("Planning updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the planning. Details: {ex.Message}");
            }
        }


        [HttpGet("get-by-id/{planningId}")]
        public async Task<IActionResult> GetPlanningById(Guid planningId)
        {
            try
            {
                var planning = await _mediator.Send(new GetPlanningByIdQuery(planningId));
                return planning != null ? Ok(planning) : NotFound("Planning not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePlanning(Guid id)
        {
            try
            {
                await _mediator.Send(new DeletePlanningCommand(id));
                return Ok("Planning deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Planning. Details: {ex.Message}");
            }
        }


    }
}
