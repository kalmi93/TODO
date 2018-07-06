using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _dbContext;

        public UnitOfWork(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit()
        {
            await _dbContext.Commit();
        }
    }
}
