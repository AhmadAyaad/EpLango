using Eplango.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eplango.Dtos
{
    public class DepartmentForCreateDto
    {

        public string DepartmentName { get; set; }
        public int? SelectedManagerId { get; set; }
        public List<Employee> NormalEmployees{ get; set; }
        public int DepartmentId { get; set; }
    }
}