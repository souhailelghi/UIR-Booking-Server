using Application.IServices;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentFeature.Queries.GetStudentByCodeUIRQuerie
{
    public class GetStudentByCodeUIRQueryHandler : IRequestHandler<GetStudentByCodeUIRQuery, Student>
    {
        private readonly IUnitOfService _unitOfService;
        public GetStudentByCodeUIRQueryHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;
            
        }
        public async Task<Student> Handle(GetStudentByCodeUIRQuery request, CancellationToken cancellationToken)
        {
            // Call the StudentService to fetch the student
            var student = await _unitOfService.StudentService.GetStudentByCodeUIRAsync(request.CodeUIR);
            return student;
        }
    }
}
