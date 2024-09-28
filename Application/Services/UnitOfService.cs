using Application.IServices;
using AutoMapper;

namespace Application.Services
{
    public class UnitOfService : IUnitOfService
    {
        public IMapper Mapper { get; set; }
        public ISportCategoryService SportCategoryService { get; set; }

        public ISportService SportService { get; set; }

        public UnitOfService(IMapper mapper , ISportCategoryService sportCategoryService , ISportService sportService)
        {
            Mapper = mapper;
            SportCategoryService = sportCategoryService;
            SportService = sportService;
        }
        
    }
}
