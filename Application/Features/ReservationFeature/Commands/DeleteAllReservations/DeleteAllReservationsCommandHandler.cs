using Application.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReservationFeature.Commands.DeleteAllReservations
{
    public class DeleteAllReservationsCommandHandler : IRequestHandler<DeleteAllReservationsCommand, string>
    {
        private readonly IUnitOfService _unitOfService;

        public DeleteAllReservationsCommandHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;
        }

        public async Task<string> Handle(DeleteAllReservationsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfService.ReservationService.DeleteAllReservationsAsync();
                return "deleted successfully";
            }
            catch (Exception ex)
            {
                // Log the exception details
                throw new ApplicationException($"An error occurred while deleting Reservations. Details: {ex.Message} {ex.StackTrace}", ex);
            }
        }
    }
}
