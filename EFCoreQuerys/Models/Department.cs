using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreQuerys.Models
{
    public class Department : RecordControl
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public List<Employee> Employees {get; set;}
    }
}