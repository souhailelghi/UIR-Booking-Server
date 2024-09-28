using Domain.Entities;
using MediatR;

namespace Application.Features.SportFeature.Commands.AddSport
{
    public class AddSportCommand : IRequest<Sport>
    {
        public Guid CategorieId { get; set; }
        public int ReferenceSport { get; set; }
        public int NbPlayer { get; set; }
        public int Daysoff { get; set; }
        public string Conditions { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
   
        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }
    }
}
