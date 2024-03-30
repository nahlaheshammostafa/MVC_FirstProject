using MVC_FirstProject.BLL.Interfaces;
using MVC_FirstProject.BLL.Repositories;
using MVC_FirstProject.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_FirstProject.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IEmployeeRepository EmployeeRepository { get; set; } = null;
        public IDepartmentRepository DepartmentRepository { get; set; } = null;

        public UnitOfWork(ApplicationDbContext dbContext) //Ask CLR to create object from "DbContext" 
        {
            _dbContext = dbContext;
            EmployeeRepository = new EmployeeRepository(_dbContext);
            DepartmentRepository = new DepartmentRepository(_dbContext);
        }
        public int Complete()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
