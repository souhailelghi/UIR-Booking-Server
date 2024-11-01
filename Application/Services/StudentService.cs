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

        public async Task<bool> CheckCodeUirExistsAsync(string codeUir)
        {
            // Check if any student exists with the given CodeUIR
            var student = await _unitOfWork.StudentRepository
                .FindAsync(p => p.CodeUIR == codeUir);

            // Return true if a matching student is found, otherwise false
            return student != null;
        }





        public async Task<Student> AddStudentAsync(Student student)
        {
            // Check if a student with the same UserId already exists
            var existingStudent = await _unitOfWork.StudentRepository
                 .FindAsync(p => p.UserId == student.UserId);

            if (existingStudent != null)
            {
                throw new InvalidOperationException("A student with the same UserId already exists.");
            }

            // Assign a new Guid to the student Id
            student.Id = Guid.NewGuid();

            // Create the new student
            await _unitOfWork.StudentRepository.CreateAsync(student);
            await _unitOfWork.CommitAsync();

            return student;
        }

        public async Task<Student> GetStudentByUserIdAsync(Guid userId)
        {
            // Assuming you have a method to fetch a student by UserId in your repository
            var student = await _unitOfWork.StudentRepository.GetByUserIdAsync(userId);

            if (student == null)
            {
                throw new KeyNotFoundException($"Student with UserId {userId} not found.");
            }

            return student;
        }

        public async Task<Student> GetStudentByIdAsync(Guid id)
        {
            Student student = await _unitOfWork.StudentRepository.GetAsNoTracking(u => u.Id == id);

            return student;
        }
    
    }
}
