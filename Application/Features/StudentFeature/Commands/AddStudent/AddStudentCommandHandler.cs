using Application.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentFeature.Commands.AddStudent
{
    public class AddStudentCommandHandler : IRequestHandler<AddStudentCommand, Student>
    {

        private readonly IUnitOfService _unitOfService;
        private readonly IMapper _mapper;

        public AddStudentCommandHandler(IUnitOfService unitOfService , IMapper mapper)
        {
            _unitOfService = unitOfService;
            _mapper = mapper;
            
        }


        public async Task<Student> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Student cannot be null.");
            }


            Student StudentMapped = _mapper.Map<Student>(request);
            Student addedStudent = await _unitOfService.StudentService.AddStudentAsync(StudentMapped);
            return addedStudent;
        }
    }
}
