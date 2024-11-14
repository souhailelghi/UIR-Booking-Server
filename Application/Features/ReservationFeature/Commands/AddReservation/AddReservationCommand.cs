using Domain.Enums;
using MediatR;

namespace Application.Features.ReservationFeature.Commands.AddReservation
{
    public class AddReservationCommand : IRequest<string>
    {
       
        public string CodeUIR { get; set; }
        public Guid SportId { get; set; }
        public Guid SportCategoryId { get; set; }
        public DateTime ReservationDate { get; set; }

        public DayOfWeekEnum DayBooking { get; set; }
        public TimeSpan HourStart { get; set; }
        public TimeSpan HourEnd { get; set; }
     
        public List<string> CodeUIRList { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }
    }
}
