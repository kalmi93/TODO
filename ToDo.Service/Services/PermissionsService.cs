using LinqKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDo.DAL;
using ToDo.DAL.Repositories;

namespace ToDo.Service.Services
{
    public interface IPermissionsService
    {
        Task<Result<bool>> UpdateTaskPermission(int id);
    }
    public class PermissionsService : BaseService, IPermissionsService
    {
        private readonly IEntityBaseRepository<DAL.Models.Task> _taskRepo;
        public PermissionsService(IEntityBaseRepository<DAL.Models.Task> taskRepo, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _taskRepo = taskRepo;
        }

        
        public async Task<Result<bool>> UpdateTaskPermission(int id)
        {
            var predicate = PredicateBuilder.New<DAL.Models.Task>();
            predicate = predicate.And(t => t.Id == id);
            predicate = predicate.And(t => t.TaskStatus == DAL.TaskStatus.Open);

            try
            {
                var task = await _taskRepo.Exists(predicate);
                return new Result<bool>(task);
            }catch(Exception ex)
            {
                return new Result<bool>(ResultStatus.EXCEPTION, ex.Message);
            }
        }
    }
}
