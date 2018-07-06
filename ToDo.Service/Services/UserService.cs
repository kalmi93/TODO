using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDo.DAL;
using ToDo.DAL.Models;
using ToDo.DAL.Repositories;

namespace ToDo.Service.Services
{
    public interface IUserService
    {
        Task<Result<List<User>>> GetAllUsers();
        Task<Result<User>> Manage(User model);
        Task<Result<User>> GetById(int id);
        Task<Result> Delete(User model);
    }
    public class UserService : BaseService, IUserService
    {
        readonly IEntityBaseRepository<User> _userRepo;

        public UserService(IEntityBaseRepository<User> userRepo, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _userRepo = userRepo;
        }

        public async Task<Result> Delete(User model)
        {
            try
            {
                _userRepo.Delete(model);
                await SaveChanges();
                return new Result(ResultStatus.OK);
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public async Task<Result<List<User>>> GetAllUsers()
        {
            try
            {
                var userList = await _userRepo.GetAll();
                return new Result<List<User>>(userList as List<User>);
            }
            catch (Exception ex)
            {
                return new Result<List<User>>(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public async Task<Result<User>> GetById(int id)
        {
            try
            {
                var user = await _userRepo.GetById(id);
                return new Result<User>(user);
            }
            catch (Exception ex)
            {
                return new Result<User>(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        //Add or Update user
        public async Task<Result<User>> Manage(User model)
        {
            if (model.Id == 0)
                _userRepo.Add(model);
            else
                _userRepo.Update(model);
            try
            {
                await SaveChanges();
                return new Result<User>(model);
            }
            catch (Exception ex)
            {
                return new Result<User>(ResultStatus.EXCEPTION, ex.Message);
            }
        }
    }
}
