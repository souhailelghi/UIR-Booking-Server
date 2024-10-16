using Application.IServices;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentFeature.Queries.GetStudntByUserIdQuerie
{
    public class GetStudntByUserIdQueryHandler : IRequestHandler<GetStudntByUserIdQuery, Student>
    {
        private readonly IUnitOfService _unitOfService;

        public GetStudntByUserIdQueryHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;
        }
        public async Task<Student> Handle(GetStudntByUserIdQuery request, CancellationToken cancellationToken)
        {
            // Call the StudentService to fetch the student
            var student = await _unitOfService.StudentService.GetStudentByUserIdAsync(request.Id);
            return student;
        }
    }
}
