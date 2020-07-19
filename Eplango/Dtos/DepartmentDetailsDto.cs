using Eplango.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eplango.Dtos
{
    public class DepartmentDetailsDto
    {
        public string DepartmentName { get; set; }
        public string DepartManager { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}