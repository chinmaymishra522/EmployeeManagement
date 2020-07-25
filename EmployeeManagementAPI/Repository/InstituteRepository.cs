using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Repository
{
    public class InstituteRepository : IInstituteRepository
    {
        private readonly ApplicationDbContext _db;

        public InstituteRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateInstitute(Institute institute)
        {
            _db.Institutes.Add(institute);
            return Save();
        }

        public bool DeleteInstitute(Institute institute)
        {
            _db.Institutes.Remove(institute);
            return Save();
        }

        public Institute GetInstitute(int instituteId)
        {
            return _db.Institutes.FirstOrDefault(a => a.Id == instituteId);

        }

        public ICollection<Institute> GetInstitutes()
        {
            return _db.Institutes.OrderBy(a => a.Name).ToList();

        }

        public bool InstituteExists(string name)
        {
            bool value = _db.Institutes.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool InstituteExists(int id)
        {
            return _db.Institutes.Any(a => a.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateInstitute(Institute institute)
        {
            _db.Institutes.Update(institute);
            return Save();
        }
    }
}
