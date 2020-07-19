using Eplango.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eplango.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(maximumLength:50)]
        public string Name { get; set; }
        public Address Address { get; set; }
        public int Phone { get; set; }
        public EmployeeType EmployeeType{ get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}