using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static EmployeeManagementAPI.Models.Employee;

namespace EmployeeManagementAPI.Models.Dtos
{
    public class EmployeeCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Age { get; set; }

        public PositionType Position { get; set; }
        [Required]
        public int InstituteId { get; set; }
        [Required]
        public double Experience { get; set; }
    }
}
