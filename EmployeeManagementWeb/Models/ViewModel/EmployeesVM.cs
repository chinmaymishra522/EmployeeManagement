using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementWeb.Models.ViewModel
{
    public class EmployeesVM
    {
        public IEnumerable<SelectListItem> InstituteList { get; set; }
        public Employee Employee { get; set; }
    }
}
