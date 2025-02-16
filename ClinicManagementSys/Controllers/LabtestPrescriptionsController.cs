//using ClinicManagementSys.Model;
//using ClinicManagementSys.Repository;
//using ClinicManagementSys.ViewModel;
//using Microsoft.AspNetCore.Mvc;

//namespace ClinicManagementSys.Controllers
//{

//    [ApiController]
//    [Route("api/[controller]")]
//    public class LabtestPrescriptionsController : Controller
//    {

//        private readonly ILabTestRepository _repository;

//        public LabtestPrescriptionsController(ILabTestRepository repository)
//        {
//            _repository = repository;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<LabTestReportViewModel>>> GetAllLabTestReports()
//        {
//            var reports = await _repository.GetAllLabTestReportsAsync();
//            return Ok(reports);
//        }

//        // Get a lab test report by ID
//        [HttpGet("{id}")]
//        public async Task<ActionResult<LabTestReportViewModel>> GetLabTestReportById(int id)
//        {
//            var report = await _repository.GetLabTestReportByIdAsync(id);
//            if (report == null) return NotFound();

//            return Ok(report);
//        }

//        // Update a lab test report
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateLabTestReport(int id, LabTestReportViewModel model)
//        {
//            if (id != model.TpId) return BadRequest("Report ID mismatch");

//            var updated = await _repository.UpdateLabTestReportAsync(id, model);
//            if (!updated) return NotFound();

//            return NoContent();






    //}
//}

