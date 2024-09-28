using Application.IServices;
using Application.IUnitOfWorks;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Student> AddStudentAsync(Student student)
        {
            student.Id = Guid.NewGuid();
            await _unitOfWork.StudentRepository.CreateAsync(student);
            await _unitOfWork.CommitAsync();
            return student;
        }
    }
}
