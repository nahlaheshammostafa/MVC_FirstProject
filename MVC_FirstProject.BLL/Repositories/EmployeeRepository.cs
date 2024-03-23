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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext)  // Ask CLR to create object from DbContext
         : base(dbContext)
        {

        }
        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _dbContext.Employees.Where(E =>E.Address.ToLower() == address.ToLower());   
        }
    }
}
