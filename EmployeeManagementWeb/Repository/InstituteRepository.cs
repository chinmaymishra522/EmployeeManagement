using EmployeeManagementWeb.Models;
using EmployeeManagementWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagementWeb.Repository
{
    public class InstituteRepository : Repository<Institute> , IInstituteRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public InstituteRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
