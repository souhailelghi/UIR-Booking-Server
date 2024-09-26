using Application.IServices;
using AutoMapper;

namespace Application.Services
{
    public class UnitOfService : IUnitOfService
    {
        public IMapper Mapper { get; set; }
        public ISportCategoryService SportCategoryService { get; set; }

        public UnitOfService(IMapper mapper , ISportCategoryService sportCategoryService)
        {
            Mapper = mapper;
            SportCategoryService = sportCategoryService;
        }
        
    }
}
