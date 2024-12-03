using Application.IRepository;
using Application.IUnitOfWorks;
using Infrastructure.Db;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ISportCategoryRepository, SportCategoryRepository>();
            services.AddScoped<ISportRepository, SportRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IPlanningRepository, PlanningRepository>();
            services.AddScoped<ITimeRangeRepository, TimeRangeRepository>();


            string? con = configuration.GetConnectionString("DefaultSQLConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(con));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
