using Application.Features.SportFeature.Commands.DeleteSport;
using Application.Features.TimeRangesFeature.Commands.DeleteTimeRange;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeRangesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimeRangesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpDelete("delete/{id}")]
       // [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DeleteTimeRange(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteTimeRangeCommand(id));
                return Ok("Sport deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Sport. Details: {ex.Message}");
            }
        }

    }
}
