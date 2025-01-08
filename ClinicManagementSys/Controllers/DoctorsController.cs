using ClinicManagementSys.Model;
using ClinicManagementSys.Repository;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClinicManagementSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorsRepository _repository;
        //DI -- constructor injection
        public DoctorsController(IDoctorsRepository repository)
        {
            _repository = repository;
        }

        #region Get all doctor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
        {
            var st = await _repository.GetAllDoctors();
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
            var st = await _repository.GetDoctorbycode(id);
            if (st == null)
            {
                return NotFound("No doctor found ");
            }
            return Ok(st);
        }
        #endregion

        #region 5 insert an doctor return doctor by id
        [HttpPost("v1")]
        public async Task<ActionResult<int>> insertDoctor(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                var newdoctorId = await _repository.insertDoctor(doctor);
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
        public async Task<ActionResult<Doctor>> updateDoctor(int id, Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                var updatedoc = await _repository.updateDoctor(id, doctor);
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
        public IActionResult Deletedoctor(int id)
        {
            try
            {
                var result = _repository.Deletedoctor(id);

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


        [HttpPost("AddDoctorAvailability")]
        public async Task<IActionResult> AddDoctorAvailability([FromBody] DoctorAvailabilityViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _repository.AddDoctorAvailabilityAsync(model);
            if (result)
                return Ok(new { Message = "Doctor availability added successfully." });

            return StatusCode(500, new { Message = "An error occurred while adding doctor availability." });
        }

        [HttpGet("SearchDoctorByPhone")]
        public async Task<IActionResult> SearchDoctorByPhone(string phoneNumber)
        {
            // Check if phoneNumber is provided
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest(new { Message = "PhoneNumber must be provided." });
            }

            // Fetch doctor details by phone number using the repository method
            var result = await _repository.GetDoctorDetailsByPhoneAsync(phoneNumber);

            // Check if doctor details were found
            if (result != null)
            {
                return NotFound(new { Message = "Doctor details not found." });
            }

            // Return the doctor details if found
            return Ok(result);
        }

    }
}
