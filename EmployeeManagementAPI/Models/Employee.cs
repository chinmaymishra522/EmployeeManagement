using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Age { get; set; }

        public enum PositionType { Instructor, Assistant_Professor, Associate_Professor, Professor }
        [Required]
        public double Experience { get; set; }
        public PositionType Position { get; set; }
        [Required]
        public int InstituteId { get; set; }

        [ForeignKey("InstituteId")]
        public Institute Institute { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
