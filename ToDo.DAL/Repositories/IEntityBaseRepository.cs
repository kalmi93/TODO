using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.DAL.Repositories
{
    public interface IEntityBaseRepository<T> where T : class, new()
    {
        void Add(T entity);
        void AddBatch(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateBatch(IEnumerable<T> entities);

        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> predicate);

        Task<T> GetById(int id);
        Task<T> GetByIncluding(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T Get(Expression<Func<T, bool>> predicate);
        

        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindByIncluding(Expression<Func<T, bool>> predicate, Func<T, object> orderBy, bool ascending, int pageIndex, int pageSize, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> FindByIncluding(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        int Count();
        int Count(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> ExecSP(string SP);

        Task<bool> Exists(Expression<Func<T, bool>> predicate);
    }
}
