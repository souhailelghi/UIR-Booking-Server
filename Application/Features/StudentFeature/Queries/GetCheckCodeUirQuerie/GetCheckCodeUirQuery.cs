using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentFeature.Queries.GetCheckCodeUirQuerie
{
    public class GetCheckCodeUirQuery:IRequest<bool>
    {
        public string CodeUIR { get; set; }

        public GetCheckCodeUirQuery(string codeUir)
        {
            CodeUIR = codeUir;
        }
    }
}
