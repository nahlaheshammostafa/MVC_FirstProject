using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_FirstProject.BLL.Interfaces;
using MVC_FirstProject.DAL.Models;
using MVC_FirstProject.PL.ViewModels;
using System;
using System.Collections.Generic;

namespace MVC_FirstProject.PL.Controllers
{
    public class DepartmentController : Controller
    {
      //  private readonly IDepartmentRepository _departmentRepo;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper, /*IDepartmentRepository departmentRepo,*/ IWebHostEnvironment env) //Ask CLR for creating an object from "IDepartmentRepository"
        {
           // _departmentRepo = departmentRepo;
           _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }
        public IActionResult Index()
        {
            var departments = _unitOfWork.Repository<Department>().GetAll();
            var mappedDep = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(mappedDep);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if(ModelState.IsValid) //Server valid validation
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _unitOfWork.Repository<Department>().Add(mappedDep);
                var count = _unitOfWork.Complete();
                if(count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(departmentVM);
        }

        //  /Department/Details/100
        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400
            var department = _unitOfWork.Repository<Department>().Get(id.Value);
            var mappedDep = _mapper.Map<Department, DepartmentViewModel>(department);
            if (department is null)
                return NotFound();  // 404
            return View(viewName, mappedDep);
        }

        //   /Department/Details/100
        //   /Department/Details
        [HttpGet]
        public IActionResult Edit(int?id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if(id != departmentVM.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(departmentVM);
            try
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.Repository<Department>().Update(mappedDep);
                _unitOfWork.Complete();
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
            return View(departmentVM);
            }
        }

        //   /Department/Details/100
        //   /Department/Details
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(DepartmentViewModel departmentVM)
        {
            try
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.Repository<Department>().Delete(mappedDep);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                // 1. Log Exception
                // 2. Friendly Message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error Ocuured during Deleting Department");
                return View(departmentVM);
            }
        }


    }
}
