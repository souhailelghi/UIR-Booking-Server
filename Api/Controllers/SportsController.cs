﻿using Application.Features.SportFeature.Commands.AddSport;
using Application.Features.SportFeature.Commands.DeleteSport;
using Application.Features.SportFeature.Commands.UpdateSport;
using Application.Features.SportFeature.Queries.GetAllSportByCategorieIdQuerie;
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
    public class SportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        //[Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> AddSport([FromForm] AddSportCommand addSportCommand)
        {
            try
            {
                if (addSportCommand == null)
                {
                    return BadRequest("Sport cannot be null.");
                } 
                // Log to check if the image is being received
                if (addSportCommand.ImageUpload != null)
                {
                    Console.WriteLine($"Received Image: {addSportCommand.ImageUpload.FileName}");
                }
                else
                {
                    Console.WriteLine("No image uploaded.");
                }

                Sport addedSport = await _mediator.Send(addSportCommand);
                return Ok(addedSport);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An error occurred while adding the Sport. Details: {ex.Message}");
            }
        }


        [HttpGet("list")]
        //[Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetSportsList()
        {
            try
            {
                List<Sport> SportsList = await _mediator.Send(new GetAllSportQuerie());
                return Ok(SportsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSport(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteSportCommand(id));
                return Ok("Sport deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Sport. Details: {ex.Message}");
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateSport([FromBody] UpdateSportCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok("Sport Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Sport. Details: {ex.Message}");
            }
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetSportById(Guid id)
        {
            try
            {
                Sport OneSport = await _mediator.Send(new GetSportByIdQuery(id));
                return Ok(OneSport);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("category/{categorieId}")]
        //[Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetSportsByCategory(Guid categorieId)
        {
            try
            {
                List<Sport> sportsList = await _mediator.Send(new GetAllSportByCategorieIdQuery(categorieId));
                return Ok(sportsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the sports. Details: {ex.Message}");
            }
        }

    }
}
