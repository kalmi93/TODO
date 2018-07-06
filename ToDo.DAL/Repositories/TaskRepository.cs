using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.DAL.Models;

namespace ToDo.DAL.Repositories
{
    public interface ITaskRepository
    {
        void Add(TaskData model);
        void Delete(TaskData model);
        void Update(TaskData model);
        Task<IEnumerable<TaskData>> GetAllIncluding(params Expression<Func<TaskData, object>>[] includeProperties);
    }
    public class TaskRepository : ITaskRepository
    {
        protected DataBaseContext _dataContext;
        protected readonly DbSet<Models.Task> _dbSet;
        public TaskRepository(DataBaseContext dbContext)
        {
            _dataContext = dbContext;
            _dbSet = dbContext.Set<Models.Task>();
        }

        public void Add(TaskData model)
        {
            _dbSet.Add(ConvertModels.TaskDataToTask(model));
        }

        public void Delete(TaskData model)
        {
            _dbSet.Remove(ConvertModels.TaskDataToTask(model));
        }

        public void Update(TaskData model)
        {
            var entity = ConvertModels.TaskDataToTask(model);
            _dbSet.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public Task<IEnumerable<TaskData>> GetAllIncluding(params Expression<Func<Models.Task, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskData>> GetAllIncluding(params Expression<Func<TaskData, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }
    }
}
