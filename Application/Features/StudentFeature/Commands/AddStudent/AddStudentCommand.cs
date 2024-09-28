using Domain.Entities;
using MediatR;

namespace Application.Features.StudentFeature.Commands.AddStudent
{
    public class AddStudentCommand : IRequest<Student>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string StudentName { get; set; }

    }
}
