using AutoMapper;
using MVC_FirstProject.DAL.Models;
using MVC_FirstProject.PL.ViewModels;

namespace MVC_FirstProject.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<DepartmentViewModel, Department>().ReverseMap();

        }
    }
}
