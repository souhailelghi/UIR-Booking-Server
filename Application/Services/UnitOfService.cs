using Application.IServices;
using AutoMapper;

namespace Application.Services
{
    public class UnitOfService : IUnitOfService
    {
        public IMapper Mapper { get; set; }
        public ISportCategoryService SportCategoryService { get; set; }

        public ISportService SportService { get; set; }

        public IStudentService StudentService {  get; set; }

        public IReservationService ReservationService {  get; set; }

        public UnitOfService(IMapper mapper , ISportCategoryService sportCategoryService , ISportService sportService , IStudentService studentService , IReservationService reservationService)
        {
            Mapper = mapper;
            SportCategoryService = sportCategoryService;
            SportService = sportService;
            StudentService = studentService;
            ReservationService = reservationService;
        }
        
    }
}
