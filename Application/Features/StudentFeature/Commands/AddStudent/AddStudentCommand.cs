using Domain.Entities;
using MediatR;

namespace Application.Features.StudentFeature.Commands.AddStudent
{
    public class AddStudentCommand : IRequest<Student>
    {
     

  
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid UserId { get; set; }
        public string CodeUIR { get; set; }

    }
}
