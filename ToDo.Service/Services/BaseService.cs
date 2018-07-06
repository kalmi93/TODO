using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDo.DAL;

namespace ToDo.Service.Services
{
    public abstract class BaseService : IBaseService
    {
        #region Properties
        protected readonly IUnitOfWork unitOfWork;
        #endregion

        public BaseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region IBaseService Members
        public virtual async Task SaveChanges()
        {
            await unitOfWork.Commit();
        }
        #endregion
    }
}
