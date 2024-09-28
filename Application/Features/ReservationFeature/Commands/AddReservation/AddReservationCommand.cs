using Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Features.ReservationFeature.Commands.AddReservation
{
    public class AddReservationCommand  : IRequest<Reservation>
    {
        
        public Guid StudentId { get; set; }
       
        public Guid SportId { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan HourStart { get; set; }
        public TimeSpan HourEnd { get; set; }

        //[ForeignKey("Student")]
        //public List<Guid> StudentIdList { get; set; }
        public List<Guid> StudentIdList { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }

    }
}
