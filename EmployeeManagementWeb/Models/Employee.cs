using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementWeb.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Age { get; set; }

        [Required]
        public double Experience { get; set; }
        public enum PositionType { Instructor, Assistant_Professor, Associate_Professor, Professor }

        public PositionType Position { get; set; }
        [Required]
        public int InstituteId { get; set; }

        public Institute Institute { get; set; }
    }
}
