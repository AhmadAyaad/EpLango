using Eplango.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eplango.Dtos
{
    public class DepartmentForListDto
    {
        public string DepartmentName { get; set; }
        public string ManagerName { get; set; }
        public int? DepartmentId { get; internal set; }
        public List<Department> Departments { get; internal set; }
        public List<Department> DeptsWithOutManagers { get; internal set; }
    }
}