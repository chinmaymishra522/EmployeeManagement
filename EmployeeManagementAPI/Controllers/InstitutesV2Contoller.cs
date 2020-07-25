using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagementAPI.Models.Dtos;
using EmployeeManagementAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/v{version:apiVersion}/institutes")]
    [ApiVersion("2.0")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class InstitutesV2Contoller : ControllerBase
    {
        private readonly IInstituteRepository _insRepo;
        private readonly IMapper _mapper;

        public InstitutesV2Contoller(IInstituteRepository insRepo, IMapper mapper)
        {
            _insRepo = insRepo;
            _mapper = mapper;
        }


        /// <summary>
        /// Get list of institutes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<InstituteDto>))]
        public IActionResult GetInstitutes()
        {
            var obj = _insRepo.GetInstitutes().FirstOrDefault();

            return Ok(_mapper.Map<InstituteDto>(obj));
        }
    }
}
