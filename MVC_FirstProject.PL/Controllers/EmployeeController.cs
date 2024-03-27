using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_FirstProject.BLL.Interfaces;
using MVC_FirstProject.DAL.Models;
using System;
using System.Linq;

namespace MVC_FirstProject.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo;
       // private readonly IDepartmentRepository _departmenteRepo;

        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository employeeRepo, IWebHostEnvironment env /*IDepartmentRepository departmentRepo*/)
        {
            _employeeRepo = employeeRepo;
            _env = env;
         //   _departmenteRepo = departmentRepo;
        }
        public IActionResult Index(string SearchInp)
        {
            TempData.Keep();
            // 1. ViewData
            ViewData["Message"] = "Hello ViewData";

            // 2. ViewBag
            //overrite for Message
            ViewBag.Message = "Hello ViewBag";

            var employees =Enumerable.Empty<Employee>();
            if(string.IsNullOrEmpty(SearchInp))
                employees = _employeeRepo.GetAll();
            else
                employees = _employeeRepo.SearchByName(SearchInp.ToLower());

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
         //   ViewData["Departments"] = _departmenteRepo.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var count = _employeeRepo.Add(employee);
                if (count > 0)
                    TempData["Message"] = "Created Successfully";
                else
                    TempData["Message"] = "Error And Not Created";
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if(!id.HasValue)
                return BadRequest();
            var employee = _employeeRepo.Get(id.Value);
            if(employee is null)
                return NotFound();
            return View(viewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
         //   ViewData["Departments"] = _departmenteRepo.GetAll();

            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if(id != employee.Id)
                return BadRequest();
            if(!ModelState.IsValid)
                return View(employee);

            try
            {
                _employeeRepo.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error Ocured During Updating Department");
                return View(employee);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)

        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        { 
            try
            {
                _employeeRepo.Delete(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error Ocured During Deleting Department");
                return View(employee);
            }
        }
    }
}
