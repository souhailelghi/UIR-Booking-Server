using Domain.Entities;
using MediatR;

namespace Application.Features.StudentFeature.Queries.GetStudntByUserIdQuerie
{
    public class GetStudntByUserIdQuery : IRequest<Student>
    {
        public Guid Id { get; set; }
        public GetStudntByUserIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
