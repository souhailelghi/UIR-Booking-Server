using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportFeature.Commands.UpdateSport
{
    public class UpdateSportCommandHanlder : IRequestHandler<UpdateSportCommand, string>
    {

        private readonly IUnitOfService _unitOfService;
        private readonly IMapper _mapper;

        public UpdateSportCommandHanlder(IUnitOfService unitOfService , IMapper mapper)
        {
           _unitOfService = unitOfService;
           _mapper = mapper;
        }
       
        public async Task<string> Handle(UpdateSportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Sport sport = _mapper.Map<Sport>(request);
                await _unitOfService.SportService.UpdateSportAsync(sport);
                return "Sport updated successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }
    }
}
