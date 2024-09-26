using Application.Features.SportCategoryFeature.Queries.GetAllSportCategoryQueries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportCategorysController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SportCategorysController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetSportCategorysList()
        {
            try
            {
                List<SportCategory> SportCategorysList = await _mediator.Send(new GetAllSportCategoryQuerie());
                return Ok(SportCategorysList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
