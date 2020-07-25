using EmployeeManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Repository.IRepository
{
    public interface IInstituteRepository
    {
        ICollection<Institute> GetInstitutes();
        Institute GetInstitute(int instituteId);
        bool InstituteExists(string name);
        bool InstituteExists(int id);
        bool CreateInstitute(Institute institute);
        bool UpdateInstitute(Institute institute);
        bool DeleteInstitute(Institute institute);
        bool Save();
    }
}
