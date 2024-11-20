using Application.Features.EmailFeature.Queries;
using Application.Features.SportCategoryFeature.Queries.GetSportCategoryById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmailController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //[HttpGet("{id}")]
        //[Authorize(Roles = "Admin,User")]
        //public async Task<IActionResult> GetSportCategoryById(Guid id)
        //{
        //    try
        //    {
        //        Email OneEmail = await _mediator.Send(new GetAllEmailQuerie(id));
        //        return Ok(OneEmail);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}
