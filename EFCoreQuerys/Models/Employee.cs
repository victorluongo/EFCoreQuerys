using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreQuerys.Models
{
    public class Employee : RecordControl
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Email {get; set;}
        public int DepartmentId {get; set;}
        public Department Department {get; set;}
    }
}