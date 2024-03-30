using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_FirstProject.BLL.Interfaces;
using MVC_FirstProject.DAL.Models;
using MVC_FirstProject.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVC_FirstProject.PL.Controllers
{
    public class EmployeeController : Controller
    {
       // private readonly IEmployeeRepository _employeeRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        // private readonly IDepartmentRepository _departmenteRepo;

        private readonly IWebHostEnvironment _env;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper, /*IEmployeeRepository employeeRepo,*/ IWebHostEnvironment env /*IDepartmentRepository departmentRepo*/)
        {
          //  _employeeRepo = employeeRepo;
          _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                employees = _unitOfWork.EmployeeRepository.GetAll();
            else
                employees = _unitOfWork.EmployeeRepository.SearchByName(SearchInp.ToLower());

            var mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmps);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitOfWork.EmployeeRepository.Add(mappedEmp);

                //1. Update Department
                //_unitOfWork.EmployeeRepository.Update(Department)

                //2. Delete Project
                //_unitOfWork.EmployeeRepository.Remove(Project)

                var count = _unitOfWork.Complete();

                if (count > 0)
                    TempData["Message"] = "Created Successfully";
                else
                    TempData["Message"] = "Error And Not Created";
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if(!id.HasValue)
                return BadRequest();
            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

            if (employee is null)
                return NotFound();
            return View(viewName, mappedEmp);
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
         //   ViewData["Departments"] = _departmenteRepo.GetAll();

            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if(id != employeeVM.Id)
                return BadRequest();
            if(!ModelState.IsValid)
                return View(employeeVM);
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitOfWork.EmployeeRepository.Update(mappedEmp);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error Ocured During Updating Department");
                return View(employeeVM);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeVM)
        { 
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error Ocured During Deleting Department");
                return View(employeeVM);
            }
        }
    }
}
