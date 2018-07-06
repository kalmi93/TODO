using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ToDo.DAL;

namespace ToDo.Web.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public TaskStatus TaskStatus { get; set; }
        [Required]
        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}
