using ClinicManagementSys.Model;
using ClinicManagementSys.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartDiagnosysController : Controller
    {
        private readonly IStartDiagnosysReository _repository;

        public StartDiagnosysController(IStartDiagnosysReository startDiagnosysRepository)
        {
            _repository = startDiagnosysRepository;
        }

        
        #region  1 -  Get all patients from DB 
        [HttpGet]
        // [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<StartDiagnosy>>> GetAllStartDiagnosy()
        {
            var patients = await _repository.GetStartDiagnosy();
            if (patients == null)
            {
                return NotFound("No Patients found");
            }
            return Ok(patients);
        }
        #endregion

        #region 2 - Get an Patient based on Id
        [HttpGet("{id}")]
        public async Task<ActionResult<StartDiagnosy>> GetAllStartDiagnosyById(int id)
        {
            var patient = await _repository.GetStartDiagnosyById(id);
            if (patient == null)
            {
                return NotFound("Oops! No Patient found on this Id");
            }
            return Ok(patient);
        }
        #endregion

        #region  3  - Insert an Patient -return Patient record
        [HttpPost]
        public async Task<ActionResult<StartDiagnosy>> InsertStartDiagnosyReturnRecord(StartDiagnosy patient)
        {
            if (ModelState.IsValid)
            {
                //insert a new record and return as an object named patient
                var newPatient = await _repository.PostStartDiagnosyReturnRecord(patient);
                if (newPatient != null)
                {
                    return Ok(newPatient);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region    4 - Update/Edit an Patient with ID
        [HttpPut("{id}")]
        public async Task<ActionResult<StartDiagnosy>> PutStartDiagnosyDetail(int id, StartDiagnosy patient)
        {
            if (ModelState.IsValid)
            {
                var updatePatient = await _repository.PutStartDiagnosy(id, patient);
                if (updatePatient != null)
                {
                    return Ok(updatePatient);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region  5  - Delete an Patient by id
        [HttpDelete("{id}")]
        public IActionResult StartDiagnosyDetail(int id)
        {
            try
            {
                var result = _repository.DeleteStartDiagnosy(id);

                if (result == null)
                {
                    //if result indicates failure or null
                    return NotFound(new
                    {
                        success = false,
                        message = "Patient could not be deleted or not found"
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
    }
}
