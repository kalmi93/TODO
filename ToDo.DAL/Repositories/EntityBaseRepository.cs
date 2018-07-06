using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.DAL.Repositories
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        #region Properties
        protected DataBaseContext _dataContext;
        protected readonly DbSet<T> _dbSet;

        #endregion

        public EntityBaseRepository(DataBaseContext dbContext)
        {
            _dataContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        #region IEntityBaseRepository Implementation
        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddBatch(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateBatch(IEnumerable<T> entities)
        {
            _dbSet.AttachRange(entities);
            foreach (var entity in entities)
                _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> objects = _dbSet.Where(predicate).AsEnumerable();
            foreach (var obj in objects)
            {
                _dbSet.Remove(obj);
            }
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.Id == id);
        }

        public virtual async Task<T> GetByIncluding(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            
            return await query.AsExpandable().FirstOrDefaultAsync(predicate);
        }


        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).FirstOrDefault();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            if (includeProperties != null)
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

            return await query.ToListAsync();
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsExpandable().Where(predicate).ToList();
        }

        public virtual IEnumerable<T> FindByIncluding(Expression<Func<T, bool>> predicate, Func<T, object> orderBy, bool ascending, int pageIndex, int pageSize, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            query = query.AsExpandable().Where(predicate);
            query = ascending ? query.OrderBy(orderBy).AsQueryable() : query.OrderByDescending(orderBy).AsQueryable();
            return PaginateSource(query, pageIndex, pageSize).ToList();
        }

        public virtual IEnumerable<T> FindByIncluding(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            query = query.AsExpandable().Where(predicate);
            return query.ToList();
        }

        public virtual int Count()
        {
            return _dbSet.Count();
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsExpandable().Where(predicate).Count();
        }
        #endregion

        protected static IQueryable<T> PaginateSource(IQueryable<T> source, int pageIndex, int pageSize)
        {
            return (source.Skip((pageIndex - 1) * pageSize).Take(pageSize));
        }

        public virtual async Task<IEnumerable<T>> ExecSP(string SP) => await _dbSet.FromSql(SP).ToListAsync();

        public virtual async Task<bool> Exists(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
    }
}
