using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Models.Dtos
{
    public class InstituteDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string District { get; set; }

        public byte[] Picture { get; set; }

        public DateTime Created { get; set; }
        public DateTime Established { get; set; }
    }
}
