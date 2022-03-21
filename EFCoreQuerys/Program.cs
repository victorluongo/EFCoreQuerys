using System;
using System.Collections.Generic;
using EFCoreQuerys.Data;
using EFCoreQuerys.Models;

namespace EFCoreQuerys
{
    class Program
    {
        static void Main(string[] args)
        {
            using var _context = new ApplicationContext();

            ConsoleTextBox("Choose an option");
            Console.WriteLine("[2] Create Database");
            Console.WriteLine("[8] Delete Database");
            Console.WriteLine("[9] Exit");
            Console.WriteLine("- - - - - - - - - - - - -\n");

            var key = Console.ReadKey();
            switch (key.KeyChar)
            {
                case '2':
                    CreateDatabase(_context);
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
