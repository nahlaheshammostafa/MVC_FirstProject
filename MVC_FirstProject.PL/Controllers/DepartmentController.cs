using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_FirstProject.BLL.Interfaces;
using MVC_FirstProject.DAL.Models;
using System;

namespace MVC_FirstProject.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentRepository departmentRepo, IWebHostEnvironment env) //Ask CLR for creating an object from "IDepartmentRepository"
        {
            _departmentRepo = departmentRepo;
            _env = env;
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
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400
            var department = _departmentRepo.Get(id.Value);
            if(department is null)
                return NotFound();  // 404
            return View(viewName, department);
        }

        //   /Department/Details/100
        //   /Department/Details
        [HttpGet]
        public IActionResult Edit(int?id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if(id != department.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(department);

            try
            {
                _departmentRepo.Update(department);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                // 1. Log Exception
                // 2. Friendly Message
                if(_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error Ocuured during Updating Department"); 
            return View(department);
            }
        }


    }
}
