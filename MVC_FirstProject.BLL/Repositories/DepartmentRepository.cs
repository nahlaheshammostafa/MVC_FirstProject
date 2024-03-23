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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext)  // Ask CLR to create object from DbContext
            : base(dbContext)
        {
            
        }
    }
}
