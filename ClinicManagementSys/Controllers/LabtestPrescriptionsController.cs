using ClinicManagementSys.Model;
using ClinicManagementSys.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSys.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LabtestPrescriptionsController : Controller
    {

        private readonly ILabTestRepository _repository;

        public LabtestPrescriptionsController(ILabTestRepository repository)
        {
            _repository = repository;
        }


        #region  1 -  Get all patients from DB 
        [HttpGet]
        // [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<TestPrescription>>> GetAllMedicineDetail()
        {
            var patients = await _repository.GetMedicineDetail();
            if (patients == null)
            {
                return NotFound("No Patients found");
            }
            return Ok(patients);
        }
        #endregion
        #region 2 - Get an Patient based on Id
        [HttpGet("{id}")]
        public async Task<ActionResult<TestPrescription>> GetAllMedicineDetailById(int id)
        {
            var patient = await _repository.GetMedicineDetailById(id);
            if (patient == null)
            {
                return NotFound("Oops! No Patient found on this Id");
            }
            return Ok(patient);
        }
        #endregion
        #region    4 - Update/Edit an Patient with ID
        [HttpPut("{id}")]
        public async Task<ActionResult<LabTestReport>> PutMedicineDetailDetail(int id, TestPrescription patient)
        {
            if (ModelState.IsValid)
            {
                var updatePatient = await _repository.PutMedicineDetail(id, patient);
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


    //}
//}

