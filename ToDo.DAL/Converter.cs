using System;
using System.Collections.Generic;
using System.Text;
using ToDo.DAL.Models;

namespace ToDo.DAL
{
    public static class ConvertModels
    {
        public static TaskData TaskToTaskData(Task model)
        {
            var taskData = new TaskData
            {
                Id = model.Id,
                Name = model.Name,
                TaskStatus = model.TaskStatus,
                UserId = model.UserId,
                Description = model.Description,
                DueDate = model.DueDate,
                User = new UserData()
                {
                    Id = model.User.Id,
                    Name = model.User.Name
                }
            };

            return taskData;
        }
        public static Task TaskDataToTask(TaskData model)
        {
            var task = new Task
            {
                Id = model.Id,
                Name = model.Name,
                TaskStatus = model.TaskStatus,
                UserId = model.UserId,
                Description = model.Description,
                DueDate = model.DueDate,
                User = new User()
                {
                    Id = model.User.Id,
                    Name = model.User.Name
                }
            };

            return task;
        }
    }
}
