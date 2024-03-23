using Microsoft.AspNetCore.Mvc;
using MVC_FirstProject.BLL.Interfaces;
using MVC_FirstProject.DAL.Models;

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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid) //Server valid validation
            {
                var count = _departmentRepo.Add(department);
                if(count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }


        //  /Department/Details/100
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest(); // 400
            var department = _departmentRepo.Get(id.Value);
            if(department is null)
                return NotFound();  // 404
            return View(department);
        }
    }
}
