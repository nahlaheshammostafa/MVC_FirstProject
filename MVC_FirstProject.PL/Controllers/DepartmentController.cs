using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_FirstProject.BLL.Interfaces;
using MVC_FirstProject.DAL.Models;
using MVC_FirstProject.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_FirstProject.PL.Controllers
{
	[Authorize]
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
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.Repository<Department>().GetAllAsync();
            var mappedDep = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(mappedDep);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if(ModelState.IsValid) //Server valid validation
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _unitOfWork.Repository<Department>().Add(mappedDep);
                var count = await _unitOfWork.Complete();
                if(count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(departmentVM);
        }

        //  /Department/Details/100
        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400
            var department = await _unitOfWork.Repository<Department>().GetAsync(id.Value);
            var mappedDep = _mapper.Map<Department, DepartmentViewModel>(department);
            if (department is null)
                return NotFound();  // 404
            return View(viewName, mappedDep);
        }

        //   /Department/Details/100
        //   /Department/Details
        [HttpGet]
        public async Task<IActionResult> Edit(int?id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if(id != departmentVM.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(departmentVM);
            try
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.Repository<Department>().Update(mappedDep);
                await _unitOfWork.Complete();
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
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentViewModel departmentVM)
        {
            try
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.Repository<Department>().Delete(mappedDep);
                await _unitOfWork.Complete();
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
