using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            this.UnitCategoryRepository = new UnitCategoryRepository(_context);
            this.UnitRepository= new UnitRepository(_context);
            this.ScheduleAppointmentRepository= new ScheduleAppointmentRepository(_context);    

		}

        public IUnitCategoryRepository UnitCategoryRepository { get; private set; }
        public IScheduleAppointmentRepository ScheduleAppointmentRepository { get; private set; }

        public IUnitRepository UnitRepository { get; set; }
        public async Task Add<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }
        public List<T> Select<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.ToList();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();

        }
        
    }
}
