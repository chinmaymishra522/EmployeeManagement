using AutoMapper;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.EmployeeMapper
{
    public class EmployeeMappings: Profile
    {
        public EmployeeMappings()
        {
            CreateMap<Institute, InstituteDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDto>().ReverseMap();
            CreateMap<Employee, EmployeeCreateDto>().ReverseMap();

        }
    }
}
