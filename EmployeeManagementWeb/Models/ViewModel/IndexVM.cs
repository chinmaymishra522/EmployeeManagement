using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementWeb.Models.ViewModel
{
    public class IndexVM
    {
        public IEnumerable<Institute> InstituteList { get; set; }
        public IEnumerable<Employee> EmployeeList { get; set; }
    }
}
