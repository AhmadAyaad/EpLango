using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eplango.Models
{
    [ComplexType]
    public class Address
    {
        [Required]
        [StringLength(maximumLength: 40)]
        public string City { get; set; }
        [Required]
        [StringLength(maximumLength: 40)]
        public string Street { get; set; }
        [Required]
        [StringLength(maximumLength: 40)]
        public string Country { get; set; }

        public override string ToString()
        {
            return $"{Street} , {City} , {Country}";
        }
    }
}