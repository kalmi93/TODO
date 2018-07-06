using System;
using System.Collections.Generic;
using System.Text;
using ToDo.DAL;

namespace ToDo.Service.Models
{
    public class TaskRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public TaskStatus TaskStatus { get; set; }
        public int UserId { get; set; }
        public UserRecord User { get; set; }
    }
}
