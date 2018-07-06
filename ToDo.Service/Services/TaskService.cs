using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDo.DAL;
using ToDo.DAL.Models;
using ToDo.DAL.Repositories;
using ToDo.Service.Models;

namespace ToDo.Service.Services
{
    public interface ITaskService
    {
        Task<Result<List<TaskRecord>>> GetAllTask();
        Task<Result<TaskRecord>> Manage(TaskRecord model);
        Task<Result<TaskRecord>> GetById(int id);
        Task<Result> Delete(TaskRecord model);

        Task<Result<List<TaskRecord>>> GetAllTask2();
    }
    public class TaskService : BaseService, ITaskService
    {
        readonly IEntityBaseRepository<DAL.Models.Task> _taskRepo;

        public TaskService(IEntityBaseRepository<DAL.Models.Task> taskRepo, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _taskRepo = taskRepo;
        }

        public async Task<Result> Delete(TaskRecord model)
        {
            var convertedModel = ConvertModels.TaskRecordToTask(model);
            try
            {
                convertedModel.UserId = 0;
                convertedModel.User = null;
                _taskRepo.Delete(convertedModel);
                await SaveChanges();
                return new Result(ResultStatus.OK);
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public async Task<Result<List<TaskRecord>>> GetAllTask()
        {
            try
            {
                var taskList = await _taskRepo.GetAllIncluding(t => t.User);
                return new Result<List<TaskRecord>>(ConvertModels.TasksToTaskRecords(taskList as List<DAL.Models.Task>));
            }catch(Exception ex)
            {
                return new Result<List<TaskRecord>>(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public async Task<Result<List<TaskRecord>>> GetAllTask2()
        {
            try
            {
                var taskList = await _taskRepo.ExecSP("GetAllTasks");
                return new Result<List<TaskRecord>>(taskList as List<TaskRecord>);
            }
            catch (Exception ex)
            {
                return new Result<List<TaskRecord>>(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public async Task<Result<TaskRecord>> GetById(int id)
        {
            try
            {
                var task = await _taskRepo.GetByIncluding(t => t.Id == id, u => u.User);
                return new Result<TaskRecord>(ConvertModels.TaskToTaskRecord(task));
            } catch(Exception ex)
            {
                return new Result<TaskRecord>(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        //Add or Update task
        public async Task<Result<TaskRecord>> Manage(TaskRecord model)
        {
            var convertedModel = ConvertModels.TaskRecordToTask(model);
            
            try
            {
                if (convertedModel.Id == 0)
                    _taskRepo.Add(convertedModel);
                else
                    _taskRepo.Update(convertedModel);
                await SaveChanges();
                return new Result<TaskRecord>(model);
            }catch(Exception ex)
            {
                return new Result<TaskRecord>(ResultStatus.EXCEPTION, ex.Message);
            }
        }
    }
}
