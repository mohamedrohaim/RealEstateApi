using DataAccessLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IUnitOfWork
    {
        IUnitCategoryRepository UnitCategoryRepository { get; }
        IUnitRepository UnitRepository { get; }
        IScheduleAppointmentRepository ScheduleAppointmentRepository { get; }
        // Task<IQueryable<T>> SelectAsync<T>(Expression<Func<T, bool>> expression) where T : class;

        List<T> Select<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class;

        Task Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task SaveAsync();
    }
}
