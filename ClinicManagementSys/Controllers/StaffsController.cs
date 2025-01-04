using ClinicManagementSys.Model;
using ClinicManagementSys.Repository;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClinicManagementSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffRepository _repository;
        //DI -- constructor injection
        public StaffsController(IStaffRepository repository)
        {
            _repository = repository;
        }
        #region Get all employees
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<ActionResult<IEnumerable<Staff>>> GetAllStaffs()
        {
            var st = await _repository.GetAllStaffs();
            if (st == null)
            {
                return NotFound("No Staff found ");
            }
            return Ok(st);
        }
        #endregion
        #region search by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> Getstaffbycode(int id)
        {
            var st = await _repository.Getstaffbycode(id);
            if (st == null)
           {
               return NotFound("No Staff found ");
           }
           return Ok(st);
        }
        #endregion
        #region 5 insert an employee return employee by id
        [HttpPost("v1")]
        public async Task<ActionResult<int>> insertstaffs(Staff staffs)
        {
            if (ModelState.IsValid)
            {
                var newstaffId = await _repository.insertstaffs(staffs);
                if (newstaffId != null)
                {
                    return Ok(newstaffId);
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
        public async Task<ActionResult<Staff>> updatestaff(int id, Staff staffs)
        {
            if (ModelState.IsValid)
            {
                var updatestaffs = await _repository.updatestaff(id, staffs);
                if (updatestaffs != null)
                {
                    return Ok(updatestaffs);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();

        }
        #endregion

        #region  7  - Delete an Employee
        [HttpDelete("{id}")]
        public IActionResult Deletestaff(int id)
        {
            try
            {
                var result = _repository.Deletestaff(id);

                if (result == null)
                {
                    //if result indicates failure or null
                    return NotFound(new
                    {
                        success = false,
                        message = "Staff could not be deleted or not found"
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexpected error occurs" });
            }
        }
        #endregion

        #region Search all
        [HttpGet("vm")]
        public async Task<ActionResult<IEnumerable<StaffDepViewModel>>> GetAllEmployeesByViewModel()
        {
            var staffs = await _repository.GetstaffDetals();
            if (staffs == null)
            {
                return NotFound("No staffs found ");
            }
            return Ok(staffs);
        }
        #endregion

        [HttpGet("SearchStaff")]
        public async Task<IActionResult> SearchStaff(int? staffId, string phoneNumber)
        {
            // Validate input: Ensure at least one of staffId or phoneNumber is provided.
            if (!staffId.HasValue && string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest(new { Message = "Either StaffId or PhoneNumber must be provided." });
            }

            try
            {
                // Retrieve staff details from the repository.
                var result = await _repository.GetStaffDetailsAsync(staffId, phoneNumber);

                // Check if staff details were found.
                if (result == null)
                {
                    return NotFound(new { Message = "Staff details not found." });
                }

                // Return the staff details.
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception (ensure logging is configured in your application).
                // Log.Error(ex, "Error while searching staff");

                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }


    }
}
