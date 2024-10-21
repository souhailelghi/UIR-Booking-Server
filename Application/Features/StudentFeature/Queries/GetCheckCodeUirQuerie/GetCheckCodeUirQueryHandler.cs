using Application.IServices;
using Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentFeature.Queries.GetCheckCodeUirQuerie
{
    public class GetCheckCodeUirQueryHandler : IRequestHandler<GetCheckCodeUirQuery, bool>
    {
        private readonly IUnitOfService _unitOfService;
        public GetCheckCodeUirQueryHandler(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;
        }
        public async Task<bool> Handle(GetCheckCodeUirQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfService.StudentService.CheckCodeUirExistsAsync(request.CodeUIR);

        }
    }
}
