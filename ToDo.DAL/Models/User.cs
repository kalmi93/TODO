using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ToDo.DAL.Models.Base;

namespace ToDo.DAL.Models
{
    public class User : EntityBase
    {
        public string Name { get; set; }
    }
}
