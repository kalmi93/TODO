using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Service;
using ToDo.Service.Models;
using ToDo.Service.Services;
using ToDo.Web.Controllers;
using ToDo.Web.Models;
using Xunit;


namespace XUnitTestProject
{
    public class HomeControllerTests
    {
        public HomeController homeController;
        public Mock<ITaskService> mockTaskService;
        public Mock<IUserService> mockUserService;
        public Mock<IPermissionsService> mockPermissionService;

        public HomeControllerTests()
        {
            mockTaskService = new Mock<ITaskService>();
            mockUserService = new Mock<IUserService>();
            mockPermissionService = new Mock<IPermissionsService>();
            homeController = new HomeController(mockTaskService.Object, mockUserService.Object, mockPermissionService.Object);
        }

        internal void Reset()
        {
            mockTaskService.Reset();
            mockUserService.Reset();
        }

        [Fact]
        public async Task UpdateTask_ReturnsBadRequest_GivenInvalidModel()
        {
            //arrange
            Reset();
            homeController.ModelState.AddModelError("error", "some error");

            //act
            var result = await homeController.UpdateTask(null);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateTask_ReturnsHttpNotFound_TaskNotFound()
        {
            Reset();

            var resultFromService = new Result<TaskRecord>(ResultStatus.EXCEPTION, "not found");
            var taskModel = new TaskModel()
            {
                Id = -1,
                TaskStatus = ToDo.DAL.TaskStatus.Open,
                Description = "",
                User = new UserModel()
                {
                    Id = -1,
                    Name = ""
                },
                UserId = -1,
                Name = "",
                DueDate = DateTime.Now
            };
            mockTaskService.Setup(s => s.Manage(It.IsAny<TaskRecord>()))
                .Returns(Task.FromResult(resultFromService));

            mockPermissionService.Setup(p => p.UpdateTaskPermission(It.IsAny<int>()))
                .Returns(Task.FromResult(new Result<bool>(true)));

            var result = await homeController.UpdateTask(taskModel);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateTask_ReturnsOk()
        {
            Reset();
            var taskModel = new TaskModel()
            {
                Id = -1,
                TaskStatus = ToDo.DAL.TaskStatus.Open,
                Description = "",
                User = new UserModel()
                {
                    Id = -1,
                    Name = ""
                },
                UserId = -1,
                Name = "",
                DueDate = DateTime.Now
            };
            var taskRecord = new TaskRecord()
            {
                Id = -1,
                TaskStatus = ToDo.DAL.TaskStatus.Open,
                Description = "",
                User = new UserRecord()
                {
                    Id = -1,
                    Name = ""
                },
                UserId = -1,
                Name = "",
                DueDate = DateTime.Now
            };

            var resultFromService = new Result<TaskRecord>(taskRecord);

            mockTaskService.Setup(s => s.Manage(It.IsAny<TaskRecord>()))
                .Returns(Task.FromResult(resultFromService));

            mockPermissionService.Setup(p => p.UpdateTaskPermission(It.IsAny<int>()))
                .Returns(Task.FromResult(new Result<bool>(true)));

            var result = await homeController.UpdateTask(taskModel);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<TaskModel>(okResult.Value);
        }

        [Fact]
        public async Task GetTask_ReturnsOk()
        {
            Reset();

            var resultFromService = new Result<List<TaskRecord>>(new List<TaskRecord>());

            mockTaskService.Setup(s => s.GetAllTask())
                .Returns(Task.FromResult(resultFromService));

            var result = await homeController.GetTasks();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetTask_ReturnsBadRequest()
        {
            Reset();

            var resultFromService = new Result<List<TaskRecord>>(ResultStatus.EXCEPTION, "not found");

            mockTaskService.Setup(s => s.GetAllTask())
                .Returns(Task.FromResult(resultFromService));

            var result = await homeController.GetTasks();

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
