using Microsoft.AspNetCore.Mvc;
using MVC_FirstProject.BLL.Interfaces;

namespace MVC_FirstProject.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;
        public DepartmentController(IDepartmentRepository departmentRepo) //Ask CLR for creating an object from "IDepartmentRepository"
        {
            _departmentRepo = departmentRepo;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepo.GetAll();
            return View(departments);
        }
    }
}
