using Application.Features.EmailFeature.Queries;
using Application.Features.SportCategoryFeature.Queries.GetSportCategoryById;
using Application.IServices;
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
        //private readonly IMediator _mediator;
        private readonly IEmailSender emailSender;

        public EmailController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }
      

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
        {
            if (emailRequest == null || string.IsNullOrWhiteSpace(emailRequest.Email))
            {
                return BadRequest("Invalid email details.");
            }

            await emailSender.SendEmailAsync(emailRequest.Email, emailRequest.Subject, emailRequest.Message);
            return Ok("Email sent successfully.");
        }
    }
    public class EmailRequest
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

    }
}
