using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.DAL
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
