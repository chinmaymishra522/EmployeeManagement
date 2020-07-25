using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementWeb.Models;
using EmployeeManagementWeb.Models.ViewModel;
using EmployeeManagementWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagementWeb.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IInstituteRepository _insRepo;
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeesController(IInstituteRepository insRepo, IEmployeeRepository employeeRepo)
        {
            _insRepo = insRepo;
            _employeeRepo = employeeRepo;
        }

        public IActionResult Index()
        {
            return View(new Employee() { });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<Institute> insList = await _insRepo.GetAllAsync(SD.InstituteAPIPath, HttpContext.Session.GetString("JWToken"));

            EmployeesVM objVM = new EmployeesVM()
            {
                InstituteList = insList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Employee = new Employee()
            };

            if (id == null)
            {
                //this will be true for Insert/Create
                return View(objVM);
            }

            //Flow will come here for update
            objVM.Employee = await _employeeRepo.GetAsync(SD.EmployeeAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));
            if (objVM.Employee == null)
            {
                return NotFound();
            }
            return View(objVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(EmployeesVM obj)
        {
            if (ModelState.IsValid)
            {

                if (obj.Employee.Id == 0)
                {
                    await _employeeRepo.CreateAsync(SD.EmployeeAPIPath, obj.Employee, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _employeeRepo.UpdateAsync(SD.EmployeeAPIPath + obj.Employee.Id, obj.Employee, HttpContext.Session.GetString("JWToken"));
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                IEnumerable<Institute> npList = await _insRepo.GetAllAsync(SD.InstituteAPIPath, HttpContext.Session.GetString("JWToken"));

                EmployeesVM objVM = new EmployeesVM()
                {
                    InstituteList = npList.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                    Employee = obj.Employee
                };
                return View(objVM);
            }
        }

        public async Task<IActionResult> GetAllEmployee()
        {
            return Json(new { data = await _employeeRepo.GetAllAsync(SD.EmployeeAPIPath, HttpContext.Session.GetString("JWToken")) });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var status = await _employeeRepo.DeleteAsync(SD.EmployeeAPIPath, id, HttpContext.Session.GetString("JWToken"));
            if (status)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            return Json(new { success = false, message = "Delete Not Successful" });
        }
    }
}
