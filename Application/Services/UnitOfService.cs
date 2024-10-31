using Application.IServices;
using Application.IUnitOfWorks;
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

        public IPlanningService PlanningService { get; }
        public ITimeRangeService TimeRangeService { get;  }

        public UnitOfService(IMapper mapper , ISportCategoryService sportCategoryService , ISportService sportService , IStudentService studentService , IReservationService reservationService , IPlanningService planningService, ITimeRangeService timeRangeService)
        {
            Mapper = mapper;
            SportCategoryService = sportCategoryService;
            SportService = sportService;
            StudentService = studentService;
            ReservationService = reservationService;
            PlanningService = planningService;
            TimeRangeService = timeRangeService;
        }

       

    }
}
