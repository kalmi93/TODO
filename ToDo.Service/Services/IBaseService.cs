using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Service.Services
{
    public interface IBaseService
    {
        Task SaveChanges();
    }
}
