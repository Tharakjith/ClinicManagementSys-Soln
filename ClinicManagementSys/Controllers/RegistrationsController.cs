﻿using ClinicManagementSys.Model;
using ClinicManagementSys.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClinicManagementSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly IRegistrationRepository _repository;
        //DI -- constructor injection
        public RegistrationsController(IRegistrationRepository repository)
        {
            _repository = repository;
        }

        #region Get all doctor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginRegistration>>> GetAllDoctors()
        {
            var st = await _repository.GetAlllogin();
            if (st == null)
            {
                return NotFound("No doctor found ");
            }
            return Ok(st);
        }
        #endregion

        #region search by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctorbycode(int id)
        {
            var st = await _repository.Getloginbycode(id);
            if (st == null)
            {
                return NotFound("No doctor found ");
            }
            return Ok(st);
        }
        #endregion

        #region 5 insert an doctor return doctor by id
        [HttpPost("v1")]
        public async Task<ActionResult<int>> insertDoctor(LoginRegistration login)
        {
            if (ModelState.IsValid)
            {
                var newdoctorId = await _repository.insertlogin(login);
                if (newdoctorId != null)
                {
                    return Ok(newdoctorId);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region update employee
        [HttpPut("{id}")]
        public async Task<ActionResult<Doctor>> updatelogin(int id, LoginRegistration login)
        {
            if (ModelState.IsValid)
            {
                var updatedoc = await _repository.updatelogin(id, login);
                if (updatedoc != null)
                {
                    return Ok(updatedoc);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region 7 - Delete an Employee
        [HttpDelete("{id}")]
        public IActionResult Deletelogin(int id)
        {
            try
            {
                var result = _repository.Deletelogin(id);

                if (result == null)
                {
                    //if result indicates failure or null
                    return new JsonResult(new
                    {
                        success = false,
                        message = "doctor could not be deleted or not found"
                    })
                    { StatusCode = StatusCodes.Status404NotFound };
                }
                return new JsonResult(new
                {
                    success = true,
                    message = "Doctor deleted successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "An unexpected error occurs",
                    error = ex.Message
                })
                { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
        #endregion

        [HttpGet("v2")]
        public async Task<ActionResult<IEnumerable<LoginRegistration>>> GetAllDepartments()
        {
            var departments = await _repository.GetTblDepartments();
            if (departments == null)
            {
                return NotFound("No Department found");
            }

            return Ok(departments);
        }
        [HttpGet("v5")]
        public async Task<ActionResult<IEnumerable<Role>>> GetAllDepartmentss()
        {
            var departments = await _repository.GetTblDepartments();
            if (departments == null)
            {
                return NotFound("No Department found");
            }

            return Ok(departments);
        }
    }
}
