using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentFeature.Queries.GetStudentByCodeUIRQuerie
{
    public class GetStudentByCodeUIRQuery : IRequest<Student>
    {
        public string CodeUIR { get; set; }

        public GetStudentByCodeUIRQuery(string codeUIR)
        {
            CodeUIR = codeUIR;
        }
        
    }
}
