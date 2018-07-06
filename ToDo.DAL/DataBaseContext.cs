using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDo.DAL.Models;
using System.Threading.Tasks;

namespace ToDo.DAL
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //later
        }

        public virtual async System.Threading.Tasks.Task Commit()
        {
            await SaveChangesAsync();
        }

        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
