using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentFeature.Queries.GetStudentByIdQuerie
{
    public class GetStudentByIdQuery : IRequest<Student>
    {
        public Guid Id { get; set; }
        public GetStudentByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
