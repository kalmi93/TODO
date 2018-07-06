using System;
using System.Collections.Generic;
using System.Text;
using ToDo.DAL.Models;
using ToDo.Service.Models;

namespace ToDo.Service
{
    public static class ConvertModels
    {
        public static TaskData TaskRecordToTaskData(TaskRecord model)
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
        public static TaskRecord TaskDataToTaskRecord(TaskData model)
        {
            var taskRecord = new TaskRecord
            {
                Id = model.Id,
                Name = model.Name,
                TaskStatus = model.TaskStatus,
                UserId = model.UserId,
                Description = model.Description,
                DueDate = model.DueDate,
                User = new UserRecord()
                {
                    Id = model.User.Id,
                    Name = model.User.Name
                }
            };

            return taskRecord;
        }

        public static Task TaskRecordToTask(TaskRecord model)
        {
            var taskData = new Task
            {
                Id = model.Id,
                Name = model.Name,
                TaskStatus = model.TaskStatus,
                UserId = model.UserId,
                Description = model.Description,
                DueDate = model.DueDate,
                
            };

            return taskData;
        }
        public static TaskRecord TaskToTaskRecord(Task model)
        {
            var taskRecord = new TaskRecord
            {
                Id = model.Id,
                Name = model.Name,
                TaskStatus = model.TaskStatus,
                UserId = model.UserId,
                Description = model.Description,
                DueDate = model.DueDate,
                User = new UserRecord()
                {
                    Id = model.User.Id,
                    Name = model.User.Name
                }
            };

            return taskRecord;
        }

        public static List<Task> TaskRecordstoTasks(List<TaskRecord> model)
        {
            var listTask = new List<Task>();
            foreach(var taskRecord in model)
            {
                listTask.Add(TaskRecordToTask(taskRecord));
            }
            return listTask;
        }

        public static List<TaskRecord> TasksToTaskRecords(List<Task> model)
        {
            var listTask = new List<TaskRecord>();
            foreach (var taskRecord in model)
            {
                listTask.Add(TaskToTaskRecord(taskRecord));
            }
            return listTask;
        }


    }
}
