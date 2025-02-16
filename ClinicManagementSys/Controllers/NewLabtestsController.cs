using ClinicManagementSys.Model;
using ClinicManagementSys.Repository;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClinicManagementSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewLabtestsController : ControllerBase
    {
        private readonly ILabTestRepositoryNew _repository;

        //DI - Dependency Injection
        public NewLabtestsController(ILabTestRepositoryNew repository)
        {
            _repository = repository;
        }

        [HttpGet("today-prescribed-tests")]
        public async Task<ActionResult<List<AppPatStaLabViewModel>>> GetTodaysPrescribedTests()
        {
            try
            {
                var tests = await _repository.GetTodaysPrescribedLabTests();
                return Ok(tests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("lab-test-details/{labTestId}")]
        public async Task<ActionResult<Labtest>> GetLabTestDetails(int labTestId)
        {
            try
            {
                var labTest = await _repository.GetLabTestDetails(labTestId);
                if (labTest == null)
                    return NotFound();
                return Ok(labTest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        [HttpPost("create-report")]
        public async Task<ActionResult<LabTestReport>> CreateLabReport([FromBody] LabTestReport report)
        {
            try
            {
                // Explicit validation
                if (report == null)
                    return BadRequest("Report cannot be null");

                if (report.LabTestId == 0)
                    return BadRequest("Invalid LabTestId");

                if (report.AppointmentId == 0)
                    return BadRequest("Invalid AppointmentId");

                var createdReport = await _repository.CreateLabTestReport(report);
                return Ok(createdReport);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpGet("report-details/{reportId}")]
        public async Task<ActionResult<LabTestReportViewModel>> GetReportDetails(int reportId)
        {
            try
            {
                var reportDetails = await _repository.GetReportDetailsByReportId(reportId);
                if (reportDetails == null)
                {
                    return NotFound();
                }

                return Ok(reportDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("bill-details/{reportId}")]
        public async Task<ActionResult<LabtestBillViewModel>> GetBillDetails(int reportId)
        {
            try
            {
                var billDetails = await _repository.GetBillDetails(reportId);
                if (billDetails == null)
                    return NotFound($"No bill found for report ID {reportId}");
                return Ok(billDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}