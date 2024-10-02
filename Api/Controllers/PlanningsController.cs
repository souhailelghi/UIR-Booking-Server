using Application.Features.PlanningFeature.Commands.AddPlanning;
using Application.Features.PlanningFeature.Queries.GetAllPlanningQuerie;
using Application.Features.PlanningFeature.Queries.GetAvailablePlanningQuerie;
using Application.Features.PlanningFeature.Queries.GetAvailableTimeRangesBySportAndDayQuerie;
using Application.Features.PlanningFeature.Queries.GetAvailableTimeRangesBySportQuerie;
using Application.Features.SportFeature.Queries.GetAllSportsQuerie;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
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


        [HttpGet("available-time-ranges")]
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

        [HttpGet("available-time-ranges-by-sport/{sportId}")]
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

        [HttpGet("available-time-ranges-by-sport-and-day/{sportId}/{day}")]
        public async Task<IActionResult> GetAvailableTimeRangesBySportAndDay(Guid sportId, DayOfWeekEnum day)
        {
            try
            {
                var availableTimeRanges = await _mediator.Send(new GetAvailableTimeRangesBySportAndDayQuery(sportId, day));

                if (availableTimeRanges == null || !availableTimeRanges.Any())
                {
                    return NotFound("No available time ranges found for the specified sport and day.");
                }

                return Ok(availableTimeRanges);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
