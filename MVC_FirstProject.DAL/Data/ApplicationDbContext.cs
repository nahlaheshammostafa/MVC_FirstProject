using Microsoft.EntityFrameworkCore;
using MVC_FirstProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVC_FirstProject.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

      // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      //  => optionsBuilder.UseSqlServer("Server = .; Database = MVCApplication; Trusted_Connectuin = True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
