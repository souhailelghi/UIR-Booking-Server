using Application.IServices;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentFeature.Queries.GetStudentByIdQuerie
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Student>
    {
        private readonly IUnitOfService _unitOfService;
        public GetStudentByIdQueryHandler(IUnitOfService unitOfService)
        {
            _unitOfService =unitOfService;
        }
        public async Task<Student> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            Student student = await _unitOfService.StudentService.GetStudentByIdAsync(request.Id);
            return student;
        }
    }
}
