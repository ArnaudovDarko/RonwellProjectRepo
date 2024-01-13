using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RonwellProject.Models
{
    public class EmployeeInfo
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }

        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? Salary { get; set; }

    }
}
