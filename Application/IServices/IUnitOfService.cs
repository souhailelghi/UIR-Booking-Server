namespace Application.IServices
{
    public interface IUnitOfService
    {
        ISportCategoryService SportCategoryService { get; }
        ISportService SportService { get; }

        IStudentService StudentService { get; }
    }
}
