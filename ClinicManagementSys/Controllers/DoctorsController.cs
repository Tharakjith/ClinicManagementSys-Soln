using ClinicManagementSys.Model;
using ClinicManagementSys.Repository;
using ClinicManagementSys.ViewModel;
using ClinicManagementSys.ViewModel.ClinicManagementSys.ViewModel;
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

        #region 7 - Delete doctor
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

        [HttpPost]
        public async Task<ActionResult<Doctor>> InsertTblEmployeesReturnRecord(Doctor employee)
        {
            if (ModelState.IsValid)
            {
                var newEmployee = await _repository.postTblEmployeesReturnRecord(employee);
                if (newEmployee != null)
                {
                    return Ok(newEmployee);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();

        }
        #region 8 Get All Departments
        [HttpGet("v2")]
        public async Task<ActionResult<IEnumerable<Specialization>>> GetAllDepartments()
        {
            var departments = await _repository.GetTblDepartments();
            if (departments == null)
            {
                return NotFound("No Specialization found");
            }

            return Ok(departments);
        }
        #endregion
        #region 8 Get All Departments
        [HttpGet("v5")]
        public async Task<ActionResult<IEnumerable<LoginRegistration>>> GetTblAllUsers()
        {
            var departments = await _repository.GetTblUsers();
            if (departments == null)
            {
                return NotFound("No Specialization found");
            }

            return Ok(departments);
        }
        #endregion
        #region 8 Get All Departments
        [HttpGet("v6")]
        public async Task<ActionResult<IEnumerable<Staff>>> GetTblstaffs()
        {
            var departments = await _repository.GetTblstaffs();
            if (departments == null)
            {
                return NotFound("No Specialization found");
            }

            return Ok(departments);
        }
        #endregion
        [HttpPost("register")]
        public IActionResult RegisterDoctor([FromBody] doctorlistnew model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Doctor model is null." });
            }

            // Call the RegisterDoctor method from the service
            bool isRegistered = _repository.RegisterDoctor(model);

            if (isRegistered)
            {
                // Return JSON output with detailed information
                return Ok(new
                {
                    status = 200,
                    message = "Doctor registered successfully.",
                    doctorDetails = model // Include relevant details from the model
                });
            }
            else
            {
                return NotFound(new
                {
                    status = 404,
                    message = "Staff or Registration not found, or Doctor role is not valid."
                });
            }
        }

        #region 8 Get All Departments
        [HttpGet("v8")]
        public async Task<ActionResult<object>> listalltimeslotsrtments()
        {
            var timeslots = await _repository.listalltimeslotsrtments();
            if (timeslots == null)
            {
                return NotFound(new { Message = "No timeslots found" });
            }

            var response = new
            {
                Value = timeslots
            };

            return Ok(response);
        }

        #endregion
        #region 8 Get All Departments
        [HttpGet("v7")]
        public async Task<ActionResult<IEnumerable<Weekday>>> listallweekdays()
        {
            var departments = await _repository.listallweekdays();
            if (departments == null)
            {
                return NotFound("No Specialization found");
            }

            return Ok(departments);
        }
        #endregion


        //AVAILABILITY

        //[HttpPost("insertAvailability/{doctorId}")]
        //public async Task<ActionResult<Availability>> InsertAvailability(int doctorId, [FromBody] Availability availability)
        //{
        //    try
        //    {
        //        // Validate input
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        // Validate Doctor
        //        var doctor = await _repository.GetDoctorByIdAsync(doctorId);
        //        if (doctor == null)
        //        {
        //            return NotFound(new { Message = "Doctor not found." });
        //        }

        //        // Validate Timeslot
        //        if (availability.TimeSlotId == null)
        //        {
        //            return BadRequest(new { Message = "TimeSlotId is required." });
        //        }

        //        var timeslot = await _repository.GetTimeslotByIdAsync(availability.TimeSlotId.Value);
        //        if (timeslot == null)
        //        {
        //            return NotFound(new { Message = "Timeslot not found." });
        //        }

        //        // Validate Session
        //        if (string.IsNullOrEmpty(availability.Session))
        //        {
        //            return BadRequest(new { Message = "Session is required." });
        //        }

        //        // Set the DoctorId in Availability
        //        availability.DoctorId = doctorId;

        //        // Insert Availability
        //        var isInserted = await _repository.InsertAvailabilityAsync(availability);
        //        if (!isInserted)
        //        {
        //            return StatusCode(500, new { Message = "Failed to insert availability." });
        //        }

        //        // Return the inserted Availability object
        //        return CreatedAtAction(
        //            nameof(InsertAvailability),
        //            new { doctorId = doctorId, id = availability.AvailabilityId },
        //            availability
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = $"An unexpected error occurred: {ex.Message}" });
        //    }
        //}


        [HttpPost("doctor/{doctorId}")]
        public async Task<IActionResult> AddAvailability(int doctorId, [FromBody] Availability availability)
        {
            try
            {
                if (doctorId != availability.DoctorId)
                {
                    return BadRequest("Doctor ID mismatch");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _repository.AddAvailability(availability);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("timeslots")]
        public async Task<IActionResult> GetAllTimeslots()
        {
            try
            {
                var timeslots = await _repository.GetAllTimeslots();
                if (timeslots == null)
                {
                    return NotFound();
                }
                return Ok(timeslots);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("timeslot/{timeSlotId}")]
        public async Task<IActionResult> GetTimeSlotDetails(int timeSlotId)
        {
            try
            {
                var timeSlot = await _repository.GetTimeSlotDetails(timeSlotId);
                if (timeSlot == null)
                {
                    return NotFound();
                }
                return Ok(new
                {
                    TimeSlotId = timeSlot.TimeSlotId,
                    StartTime = timeSlot.StartTime,
                    EndTime = timeSlot.EndTime,
                    WeekdayName = timeSlot.Weekdays?.WeekdaysName
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}