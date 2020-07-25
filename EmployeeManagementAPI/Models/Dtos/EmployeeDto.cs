using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static EmployeeManagementAPI.Models.Employee;

namespace EmployeeManagementAPI.Models.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Age { get; set; }

        [Required]
        public double Experience { get; set; }
        public PositionType Position { get; set; }
        [Required]
        public int InstituteId { get; set; }

        public InstituteDto Institute { get; set; }


    }
}
