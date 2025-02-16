using ClinicManagementSys.Model;
using ClinicManagementSys.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClinicManagementSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabsController : ControllerBase
    {
        private readonly ILabRepository _repository;

        // Dependency Injection - Constructor Injection
        public LabsController(ILabRepository repository)
        {
            _repository = repository;
        }

        #region Get all tests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Labtest>>> GetAllLabtest()
        {
            var st = await _repository.GetAllLabtest();
            if (st == null)
            {
                return NotFound("No test found");
            }
            return Ok(st);
        }
        #endregion

        #region Search by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Labtest>> GetLabtestbycode(int id)
        {
            var st = await _repository.GetLabtestbycode(id);
            if (st == null)
            {
                return NotFound("No tests found");
            }
            return Ok(st);
        }
        #endregion

        #region Insert a doctor and return doctor by ID
        [HttpPost("v1")]
        public async Task<ActionResult<int>> InsertLabtest(Labtest labtests)
        {
            if (ModelState.IsValid)
            {
                var newtestId = await _repository.insertLabtest(labtests);
                if (newtestId != null)
                {
                    return Ok(newtestId);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region Update employee
        [HttpPut("{id}")]
        public async Task<ActionResult<Labtest>> updateLabtestf(int id, Labtest labtests)
        {
            if (ModelState.IsValid)
            {
                var updatestaffs = await _repository.updateLabtestf(id, labtests);
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

        #region Delete an employee
        [HttpDelete("{id}")]
        public IActionResult Deletelabtests(int id)
        {
            try
            {
                var result = _repository.Deletelabtests(id);

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

        [HttpPost]
        public async Task<ActionResult<Labtest>> InsertTblEmployeesReturnRecord(Labtest employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid employee data.");
            }

            var newEmployee = await _repository.postTblEmployeesReturnRecord(employee);
            if (newEmployee != null)
            {
                return Ok(newEmployee);
            }

            return StatusCode(500, "An error occurred while saving the employee.");
        }
    }
}