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
    [Route("api/v{version:apiVersion}/institutes")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public class InstitutesController : ControllerBase
    {
        private readonly IInstituteRepository _insRepo;
        private readonly IMapper _mapper;

        public InstitutesController(IInstituteRepository insRepo, IMapper mapper)
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
            var objList = _insRepo.GetInstitutes();
            var objDto = new List<InstituteDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<InstituteDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get individual institute
        /// </summary>
        /// <param name="instituteId"> The Id of the institute </param>
        /// <returns></returns>
        [HttpGet("{instituteId:int}", Name = "GetInstitute")]
        [ProducesResponseType(200, Type = typeof(InstituteDto))]
        [ProducesResponseType(404)]
        [Authorize]
        [ProducesDefaultResponseType]
        public IActionResult GetInstitute(int instituteId)
        {
            var obj = _insRepo.GetInstitute(instituteId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<InstituteDto>(obj);
            return Ok(objDto);

        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(InstituteDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateInstitute([FromBody] InstituteDto instituteDto)
        {
            if (instituteDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_insRepo.InstituteExists(instituteDto.Name))
            {
                ModelState.AddModelError("", "Institute Exists!");
                return StatusCode(404, ModelState);
            }
            var instituteObj = _mapper.Map<Institute>(instituteDto);
            if (!_insRepo.CreateInstitute(instituteObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {instituteObj.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetInstitute", new { instituteId = instituteObj.Id }, instituteObj);
        }

        [HttpPatch("{instituteId:int}", Name = "UpdateInstitute")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateInstitute(int instituteId, [FromBody] InstituteDto instituteDto)
        {
            if (instituteDto == null || instituteId != instituteDto.Id)
            {
                return BadRequest(ModelState);
            }

            var instituteObj = _mapper.Map<Institute>(instituteDto);
            if (!_insRepo.UpdateInstitute(instituteObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {instituteObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


        [HttpDelete("{instituteId:int}", Name = "DeleteInstitute")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteInstitute(int instituteId)
        {
            if (!_insRepo.InstituteExists(instituteId))
            {
                return NotFound();
            }

            var instituteObj = _insRepo.GetInstitute(instituteId);
            if (!_insRepo.DeleteInstitute(instituteObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {instituteObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
