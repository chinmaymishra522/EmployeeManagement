using EmployeeManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Repository.IRepository
{
    public interface IEmployeeRepository
    {
        ICollection<Employee> GetEmployees();
        ICollection<Employee> GetEmployeesInInstitute(int insId);
        Employee GetEmployee(int employeeId);
        bool EmployeeExists(string name);
        bool EmployeeExists(int id);
        bool CreateEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(Employee employee);
        bool Save();
    }
}
