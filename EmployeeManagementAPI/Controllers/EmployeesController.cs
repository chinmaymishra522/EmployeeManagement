using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Models.Dtos;
using EmployeeManagementAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/v{version:apiVersion}/employees")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public class EmployeesController : ControllerBase
    {

        private readonly IEmployeeRepository _employeeRepo;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepo, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }


        /// <summary>
        /// Get list of employees.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<EmployeeDto>))]
        public IActionResult GetEmployees()
        {
            var objList = _employeeRepo.GetEmployees();
            var objDto = new List<EmployeeDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<EmployeeDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get individual employee
        /// </summary>
        /// <param name="employeeId"> The id of the employee </param>
        /// <returns></returns>
        [HttpGet("{employeeId:int}", Name = "GetEmployee")]
        [ProducesResponseType(200, Type = typeof(EmployeeDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = "Admin")]
        public IActionResult GetEmployee(int employeeId)
        {
            var obj = _employeeRepo.GetEmployee(employeeId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<EmployeeDto>(obj);

            return Ok(objDto);

        }

        [HttpGet("[action]/{instituteId:int}")]
        [ProducesResponseType(200, Type = typeof(EmployeeDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetEmployeesInInstitute(int instituteId)
        {
            var objList = _employeeRepo.GetEmployeesInInstitute(instituteId);
            if (objList == null)
            {
                return NotFound();
            }
            var objDto = new List<EmployeeDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<EmployeeDto>(obj));
            }


            return Ok(objDto);

        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTrail([FromBody] EmployeeCreateDto employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_employeeRepo.EmployeeExists(employeeDto.Name))
            {
                ModelState.AddModelError("", "Employee Exists!");
                return StatusCode(404, ModelState);
            }
            var employeeObj = _mapper.Map<Employee>(employeeDto);
            if (!_employeeRepo.CreateEmployee(employeeObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {employeeObj.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetEmployee", new { employeeId = employeeObj.Id }, employeeObj);
        }

        [HttpPatch("{employeeId:int}", Name = "UpdateEmployee")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateEmployee(int employeeId, [FromBody] EmployeeUpdateDto employeeDto)
        {
            if (employeeDto == null || employeeId != employeeDto.Id)
            {
                return BadRequest(ModelState);
            }

            var employeeObj = _mapper.Map<Employee>(employeeDto);
            if (!_employeeRepo.UpdateEmployee(employeeObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {employeeObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


        [HttpDelete("{employeeId:int}", Name = "DeleteEmployee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteEmployee(int employeeId)
        {
            if (!_employeeRepo.EmployeeExists(employeeId))
            {
                return NotFound();
            }

            var employeeObj = _employeeRepo.GetEmployee(employeeId);
            if (!_employeeRepo.DeleteEmployee(employeeObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {employeeObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}
