using Application.IServices;
using Application.Mapping;
using Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


using System.Reflection;

namespace Application
{
    public static class ApplicationContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            // Register the Unit of Service
            services.AddScoped<IUnitOfService, UnitOfService>();
            services.AddTransient<ISportCategoryService, SportCategoryService>();
            services.AddTransient<ISportService, SportService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IReservationService, ReservationService>();


         
            //configuration of mediator
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //configuration of auto mapper
            services.AddAutoMapper(typeof(AutoMapperProfile));
            return services;
        }
    }
}
