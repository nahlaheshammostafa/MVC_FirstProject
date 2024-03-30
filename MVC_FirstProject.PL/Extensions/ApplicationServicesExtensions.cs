using Microsoft.Extensions.DependencyInjection;
using MVC_FirstProject.BLL;
using MVC_FirstProject.BLL.Interfaces;
using MVC_FirstProject.BLL.Repositories;

namespace MVC_FirstProject.PL.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
    }
}
