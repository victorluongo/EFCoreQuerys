using System;
using System.Collections.Generic;
using System.Linq;
using EFCoreQuerys.Data;
using EFCoreQuerys.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreQuerys
{
    class Program
    {
        static void Main(string[] args)
        {
            using var _context = new ApplicationContext();

            ConsoleTextBox("Choose an option");
            Console.WriteLine("[2] Create Database");
            Console.WriteLine("[3] Employees Global Filter List");
            Console.WriteLine("[4] Employees Ignore Global Filter List");
            Console.WriteLine("[5] Employees Interpolated List");
            Console.WriteLine("[6] Departments Projected List");
            Console.WriteLine("[7] Departments Slipt Query");
            Console.WriteLine("[8] Delete Database");
            Console.WriteLine("[9] Exit");
            Console.WriteLine("- - - - - - - - - - - - -\n");

            var key = Console.ReadKey();
            switch (key.KeyChar)
            {
                case '2':
                    CreateDatabase(_context);
                    break;

                case '3':
                    EmployeesGlobalFilterList(_context);
                    break;

                case '4':
                    EmployeesIgnoreGlobalFilterList(_context);
                    break;

                case '5':
                    EmployeesInterpolatedList(_context);
                    break;

                case '6':
                    DepartmentsProjectedList(_context);
                    break;
                
                case '7':
                    DepartmentsSliptQuery(_context);
                    break;

                case '8':
                    DeleteDatabase(_context);
                    break;
                
                case '9':
                    Environment.Exit(0);
                    break;                       
            }

            Main(args);
        }

        static bool HealthCheck(ApplicationContext _context)
        {
            bool healthCheck = _context.Database.CanConnect();
            
            return healthCheck;
        }

        static void EmployeesGlobalFilterList(ApplicationContext _context)
        {
            
            if(HealthCheck(_context)){
            
                var employees = _context.Employees
                                .ToList();

                EmployeesList(employees);

            } else {

                ConsoleTextBox("Database not found.");

            }

        }

        static void EmployeesIgnoreGlobalFilterList(ApplicationContext _context)
        {
            
            if(HealthCheck(_context)){
            
                var employees = _context.Employees
                                .IgnoreQueryFilters()
                                .ToList();

                EmployeesList(employees);

            } else {

                ConsoleTextBox("Database not found.");

            }

        }

        static void EmployeesInterpolatedList(ApplicationContext _context)
        {

            if(HealthCheck(_context)){
            
                bool IsDeleted = true;

                var employees = _context.Employees
                                .FromSqlInterpolated($"SELECT * FROM Employees WHERE IsDeleted={IsDeleted}")
                                .IgnoreQueryFilters()
                                .ToList();

                EmployeesList(employees);

            } else {

                ConsoleTextBox("Database not found.");

            }

        }

        static void DepartmentsProjectedList(ApplicationContext _context)
        {

            if(HealthCheck(_context)){

                var departments = _context.Departments
                                .Where(p => p.IsDeleted==false)
                                .Select(p => new {
                                    p.Name,
                                    Employees = p.Employees.Select(e => new {
                                        e.Name,
                                        e.IsDeleted
                                    }).Where(e => e.IsDeleted==false)
                                })
                                .IgnoreQueryFilters()
                                .ToList();
                
                foreach(var department in departments)
                {
                    ConsoleTextBox(department.Name);
                    
                    List<Employee> employees = new List<Employee>();
                    
                    foreach(var employee in department.Employees)
                    {
                        employees.Add(
                            new Employee
                            {
                                Name = employee.Name,
                                IsDeleted = employee.IsDeleted
                            }
                        );
                    }

                    EmployeesList(employees, false, false);

                    employees.Clear();

                }

            } else {

                ConsoleTextBox("Database not found.");

            }

        }

        static void DepartmentsSliptQuery(ApplicationContext _context)
        {

            if(HealthCheck(_context)){

                var departments = _context.Departments
                                .Include(p => p.Employees.Where(e => e.IsDeleted==false))
                                .Where(p => p.IsDeleted==false)
                                .AsSplitQuery()
                                .IgnoreQueryFilters()
                                .ToList();
                
                foreach(var department in departments)
                {
                    ConsoleTextBox(department.Name);
                    
                    List<Employee> employees = new List<Employee>();
                    
                    foreach(var employee in department.Employees)
                    {
                        employees.Add(
                            new Employee
                            {
                                Name = employee.Name,
                                IsDeleted = employee.IsDeleted
                            }
                        );
                    }

                    EmployeesList(employees, false, false);

                    employees.Clear();

                }

            } else {

                ConsoleTextBox("Database not found.");

            }

        }        

        static void EmployeesList(List<Employee> employees, 
                                    bool ShowTitle = true, 
                                    bool ShowFieldsTitles = true, 
                                    bool ShowRecordCount = true)
        {

            if(ShowTitle) {
                ConsoleTextBox("Employees");
            }

            if(ShowFieldsTitles) {
                Console.WriteLine("IsDeleted | Name");
                Console.WriteLine("- - - - - - - - - - - - -");
            }

            foreach(var employee in employees)
            {
                Console.WriteLine($"{employee.IsDeleted} \t    {employee.Name}");
            }

            if(ShowRecordCount) {
                Console.WriteLine("- - - - - - - - - - - - -");
                Console.WriteLine($"{employees.Count()} record(s) found.");
            }

        }

        static void CreateDatabase(ApplicationContext _context) 
        {
            if (!HealthCheck(_context)) {
                
                if(_context.Database.EnsureCreated()){

                    ConsoleTextBox("Database created.");

                    _context.Departments.AddRange(

                        new Department 
                        {
                            Name = "Customer Support",
                            Employees = new List<Employee> 
                            {
                                new Employee 
                                {
                                    Name = "James Murphy",
                                    Email = "jamesmurphy@globaltech.com"
                                },

                                new Employee 
                                {
                                    Name = "Lauren Smith",
                                    Email = "laurensmith@globaltech.com"
                                },

                                new Employee
                                {
                                    Name = "Tracy Miler",
                                    Email = "tracymiler@globaltech.com",
                                    IsDeleted = true
                                },

                                new Employee
                                {
                                    Name = "Oscar Wilson",
                                    Email = "oscarwilson@globaltech.com"
                                },

                                new Employee
                                {
                                    Name = "Roy Taylor",
                                    Email = "roytaylor@globaltech.com",
                                    IsDeleted = true
                                },                                
                            }
                            
                        },

                        new Department 
                        {
                            Name = "Sales",
                            Employees = new List<Employee>
                            {
                                new Employee
                                {
                                    Name = "Megan O' Sullivan",
                                    Email = "meganosullivan@globaltech.com"
                                },

                                new Employee
                                {
                                    Name = "Thomas Walsh",
                                    Email = "thomaswalsh@globaltech.com"
                                },
                                
                                new Employee
                                {
                                    Name = "Lee Wang",
                                    Email = "leewang@globaltech.com",
                                    IsDeleted = true
                                },

                                new Employee
                                {
                                    Name = "Emily Willians",
                                    Email = "emilywillians@globaltech.com"
                                },                                                               

                            }
                        }

                    );

                    _context.SaveChanges();

                    ConsoleTextBox("Sample Records Added.");

                } else {

                    ConsoleTextBox("Database not created.");

                }
    
            } else {

                ConsoleTextBox("Database already exists!");

            }
        }

        static void DeleteDatabase(ApplicationContext _context) 
        {
            if (!HealthCheck(_context)) {
                
                ConsoleTextBox("Database not found!");

            } else {

                _context.Database.EnsureDeleted();

                ConsoleTextBox("Database deleted.");

            }
        }        

        static void ConsoleTextBox(string _text)      
        {
            var _consoleTextBox  = "\n";
                _consoleTextBox += "\n- - - - - - - - - - - - -\n";
                _consoleTextBox += _text;
                _consoleTextBox += "\n- - - - - - - - - - - - -";
            
            Console.WriteLine (_consoleTextBox);
        }        
    }
}
