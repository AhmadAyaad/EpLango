using Eplango.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eplango.Dtos
{
    public class EmployeeForCreateDto
    {
        public int SelectedDepartmentId { get; set; }
        public List<Department> Departments { get; set; }
        public Department Department { get; set; }
    
        public Employee Employee { get; set; }

       
    }
}