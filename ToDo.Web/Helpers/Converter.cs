using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Service.Models;
using ToDo.Web.Models;

namespace ToDo.Web.Helpers
{
    public static class ConvertModels
    {
        public static TaskModel TaskRecordToTaskModel(TaskRecord model)
        {
            var taskData = new TaskModel
            {
                Id = model.Id,
                Name = model.Name,
                TaskStatus = model.TaskStatus,
                UserId = model.UserId,
                Description = model.Description,
                DueDate = model.DueDate,
            };
            if (model.User != null)
                taskData.User = new UserModel
                {
                    Id = model.User.Id,
                    Name = model.User.Name
                };
            return taskData;
        }
        public static TaskRecord TaskModelToTaskRecord(TaskModel model)
        {
            var taskRecord = new TaskRecord
            {
                Id = model.Id,
                Name = model.Name,
                TaskStatus = model.TaskStatus,
                UserId = model.UserId,
                Description = model.Description,
                DueDate = model.DueDate,
                
            };
            if (model.User != null)
                taskRecord.User = new UserRecord
                {
                    Id = model.User.Id,
                    Name = model.User.Name
                };
            return taskRecord;
        }
        public static List<TaskModel> TaskRecordsToTaskModels(List<TaskRecord> model)
        {
            var list = new List<TaskModel>();
            foreach(var task in model)
            {
                list.Add(TaskRecordToTaskModel(task));
            }
            return list;
        }
    }
}
