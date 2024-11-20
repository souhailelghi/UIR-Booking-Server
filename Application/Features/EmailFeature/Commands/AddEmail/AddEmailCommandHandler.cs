using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.EmailFeature.Commands.AddEmail
{
    public class AddEmailCommandHandler : IRequestHandler<AddEmailCommand, Email>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IMapper _mapper;

        public Task<Email> Handle(AddEmailCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        //public async Task<Email> Handle(AddEmailCommand request, CancellationToken cancellationToken)
        //{
        //    if (request == null)
        //    {
        //        throw new ArgumentNullException(nameof(request), "Email cannot be null.");
        //    }

        //    // Map the AddSportCommand to the Sport entity
        //    Email emailMapped = _mapper.Map<Email>(request);

        //    // Add the sport using the service and return the result
        //    Email addedEmail = await _unitOfService.emailService.AddEmailAsync(emailMapped);
        //    return addedemail;
        //}
    }
}
