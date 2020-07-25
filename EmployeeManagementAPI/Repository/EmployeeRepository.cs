using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateEmployee(Employee employee)
        {
            _db.Employees.Add(employee);
            return Save();
        }

        public bool DeleteEmployee(Employee employee)
        {
            _db.Employees.Remove(employee);
            return Save();
        }

        public Employee GetEmployee(int employeeId)
        {
            return _db.Employees.Include(c => c.Institute).FirstOrDefault(a => a.Id == employeeId);
        }

        public ICollection<Employee> GetEmployees()
        {
            return _db.Employees.Include(c => c.Institute).OrderBy(a => a.Name).ToList();
        }

        public bool EmployeeExists(string name)
        {
            bool value = _db.Employees.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool EmployeeExists(int id)
        {
            return _db.Employees.Any(a => a.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateEmployee(Employee employee)
        {
            _db.Employees.Update(employee);
            return Save();
        }

        public ICollection<Employee> GetEmployeesInInstitute(int insId)
        {
            return _db.Employees.Include(c => c.Institute).Where(c => c.InstituteId == insId).ToList();

        }
    }
}
