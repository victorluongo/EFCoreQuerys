using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreQuerys.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreQuerys.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Employee> Employees {get; set;}
        public DbSet<Department> Departments {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Data source=SQLiteDatabase.sqlite")
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasQueryFilter(p => !p.IsDeleted);
        }
    }
}