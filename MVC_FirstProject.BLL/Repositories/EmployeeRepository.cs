using Microsoft.EntityFrameworkCore;
using MVC_FirstProject.BLL.Interfaces;
using MVC_FirstProject.DAL.Data;
using MVC_FirstProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_FirstProject.BLL.Repositories
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private ApplicationDbContext _dbContext; //NULL
        public EmployeeRepository(ApplicationDbContext dbContext)  //Ask CLR for creating object from "ApplicationDbContext"
        {
            _dbContext = dbContext;
        }
        public int Add(Employee entity)
        {
            _dbContext.Employees.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(Employee entity)
        {
            _dbContext.Employees.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public Employee Get(int id)
        {
            return _dbContext.Find<Employee>(id); //EF Core 3.1 NEW Feature
        }

        public IEnumerable<Employee> GetAll()
            => _dbContext.Employees.AsNoTracking().ToList();

        public int Update(Employee entity)
        {
            _dbContext.Employees.Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
