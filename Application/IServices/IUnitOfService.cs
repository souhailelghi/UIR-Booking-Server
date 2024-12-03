namespace Application.IServices
{
    public interface IUnitOfService
    {
        ISportCategoryService SportCategoryService { get; }
        ISportService SportService { get; }

        IStudentService StudentService { get; }
        IEventService EventService { get; }

        IReservationService ReservationService { get; }
        IPlanningService PlanningService { get; }
        ITimeRangeService TimeRangeService { get; }




    }
}
