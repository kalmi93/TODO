using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.DAL.Models
{
    public class TaskData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public TaskStatus TaskStatus { get; set; }
        public int UserId { get; set; }
        public UserData User { get; set; }
    }
}
