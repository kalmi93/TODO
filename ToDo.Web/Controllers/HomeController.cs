using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDo.DAL.Models;
using ToDo.Service.Services;
using ToDo.Web.Helpers;
using ToDo.Web.Models;

namespace ToDo.Web.Controllers
{
    public class HomeController : Controller
    {
        readonly ITaskService taskService;
        readonly IUserService userService;
        readonly IPermissionsService permissionsService;

        public HomeController(ITaskService taskService, IUserService userService, IPermissionsService permissionsService)
        {
            this.taskService = taskService;
            this.userService = userService;
            this.permissionsService = permissionsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        [HttpGet("/api/tasks")]
        public async Task<IActionResult> GetTasks()
        {
            var taskList =  await taskService.GetAllTask();
            return taskList.IsOk() ? 
                (IActionResult) Ok(ConvertModels.TaskRecordsToTaskModels(taskList.Data)) : 
                BadRequest(taskList.Message);
        }

        [HttpGet("/api/tasks/{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var taskList = await taskService.GetById(id);
            return taskList.IsOk() ? 
                (IActionResult)Ok(ConvertModels.TaskRecordToTaskModel(taskList.Data)) : 
                NotFound(taskList.Message);
        }

        [HttpPost("/api/tasks")]
        public async Task<IActionResult> AddTask([FromBody]TaskModel task)
        {
            var newTask = await taskService.Manage(ConvertModels.TaskModelToTaskRecord(task));
            if (newTask.IsOk())
                return Ok(newTask.Data);
            else
                return BadRequest(newTask.Message);
        }

        [HttpDelete("/api/tasks/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await taskService.GetById(id);
            if (!task.IsOk())
                return NotFound();
            var deleteRes = await taskService.Delete(task.Data);
            if (deleteRes.Status != Service.ResultStatus.OK)
                return NotFound();
            return Ok();
        }

        [HttpPut("/api/tasks")]
        public async Task<IActionResult> UpdateTask([FromBody]TaskModel task)
        {


            int asd = 5;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var canUpdate = await permissionsService.UpdateTaskPermission(task.Id);
            if (!canUpdate.Data)
                return BadRequest(error: "can't update, task is already closed");

            var newTask = await taskService.Manage(ConvertModels.TaskModelToTaskRecord(task));
            return newTask.IsOk() ? (IActionResult)Ok(ConvertModels.TaskRecordToTaskModel(newTask.Data)) : NotFound(newTask.Message);
        }

        [HttpGet("/api/getusers")]
        public async Task<IActionResult> GetUsers()
        {
            var userList = await userService.GetAllUsers();
            if (userList.IsOk())
                return Ok(userList.Data);
            else
                return NotFound(userList.Message);
        }
    }
}
