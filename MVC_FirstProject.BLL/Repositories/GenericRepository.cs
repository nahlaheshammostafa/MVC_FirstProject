using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;
        public GenericRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
           // return _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
          //  return _dbContext.SaveChanges();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbContext.FindAsync<T>(id); //EF Core 3.1 NEW Feature
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _dbContext.Employees.Include(E => E.Department).AsNoTracking().ToListAsync();
            }
            else return await _dbContext.Set<T>().AsNoTracking().ToListAsync();

        }
        public void Update(T entity)
        {
            _dbContext.Update(entity);
           // return _dbContext.SaveChanges();
        }
    }
}
