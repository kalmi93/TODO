using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.DAL;
using ToDo.DAL.Repositories;
using ToDo.Service;
using ToDo.Service.Models;
using ToDo.Service.Services;
using Xunit;

namespace XUnitTestProject
{
    public class TaskServiceTests
    {
        public TaskService taskService;
        public Mock<IEntityBaseRepository<ToDo.DAL.Models.Task>> taskRepository;
        public Mock<IUnitOfWork> unitOfWork;

        public TaskServiceTests()
        {
            taskRepository = new Mock<IEntityBaseRepository<ToDo.DAL.Models.Task>>();
            unitOfWork = new Mock<IUnitOfWork>();
            taskService = new TaskService(taskRepository.Object,unitOfWork.Object);
        }


        [Fact]
        public async Task GetAllTask_ReturnsListOfTasks()
        {
            ///arrange
            IEnumerable<ToDo.DAL.Models.Task> expectedList = new List<ToDo.DAL.Models.Task>();

            taskRepository.Setup(t => t.GetAllIncluding(u => u.User))
                .Returns(Task.FromResult(expectedList));

            //act
            var actualList = await taskService.GetAllTask();

            //assert
            Assert.IsType<Result<List<TaskRecord>>>(actualList);
            Assert.IsType<List<TaskRecord>>(actualList.Data);
        }

        [Fact]
        public async Task GetAllTask_ReturnsError_ThrowsExeption()
        {
            taskRepository.Setup(t => t.GetAllIncluding(u => u.User))
                .Throws<Exception>();

            var result = await taskService.GetAllTask();

            Assert.IsType<Result<List<TaskRecord>>>(result);
            Assert.Equal(ResultStatus.EXCEPTION, result.Status);
        }

        [Fact]
        public async Task GetTaskById_ReturnsTask()
        {
            int id = 123;
            var task = new ToDo.DAL.Models.Task()
            {
                Id = 123,
                User = new ToDo.DAL.Models.User()
                {
                    Id = 0,
                    Name = ""
                },
                UserId = 0,
                Name = "",
                Description = "",
                DueDate = DateTime.Now,
                TaskStatus = ToDo.DAL.TaskStatus.NotSet
            };
            
            taskRepository.Setup(t => t.GetByIncluding(It.IsAny<Expression<Func<ToDo.DAL.Models.Task, bool>>>(), It.IsAny<Expression<Func<ToDo.DAL.Models.Task, object>>[]>()))
                .Returns(Task.FromResult(task));

            var result = await taskService.GetById(id);

            Assert.IsType<Result<TaskRecord>>(result);
            Assert.IsType<TaskRecord>(result.Data);
        }

        [Fact]
        public async Task GetTaskById_ReturnsError_ThrowsExeption()
        {
            int id = 123;
            
            taskRepository.Setup(t => t.GetByIncluding(It.IsAny<Expression<Func<ToDo.DAL.Models.Task, bool>>>(), It.IsAny<Expression<Func<ToDo.DAL.Models.Task, object>>[]>()))
                .Throws<Exception>();

            var result = await taskService.GetById(id);

            Assert.IsType<Result<TaskRecord>>(result);
            Assert.Equal(ResultStatus.EXCEPTION, result.Status);
        }
    }
}
