using Eplango.ConfigurationClasses;
using Eplango.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Eplango.Data
{
    public class DataContext : DbContext
    {

        public DataContext()
            : base("name=eplangoConnectionString")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments{ get; set; }
    }
}