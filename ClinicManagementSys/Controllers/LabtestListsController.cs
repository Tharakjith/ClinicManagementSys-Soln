using ClinicManagementSys.Repository;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClinicManagementSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabtestListsController : ControllerBase
    {
        ////Call repository
        //private readonly ILabtestListRepository _repository;

        ////DI Constructor Injection
        //public LabtestListsController(ILabtestListRepository repository)
        //{
        //    _repository = repository;
        //}


        //#region 1- get all labtests-search all
        //[HttpGet("vm")]
        //public async Task<ActionResult<IEnumerable<AppPatStaLabViewModel>>> GetAllLabtestByViewModel()
        //{
        //    var labtests = await _repository.GetViewModelLabtestList();
        //    if (labtests == null || !labtests.Value.Any())
        //    {
        //        return NotFound("No Labtests found for today's appointments.");
        //    }

        //    return Ok(labtests);
        //}

        //#endregion

        //#region 2- get all labtests-search by id
        //[HttpGet("{id}")]
        //public async Task<ActionResult<AppPatStaLabViewModel>> GetLabtestById(int tokenNumber)
        //{
        //    var labtests = await _repository.GetLabtestById(tokenNumber);
        //    if (labtests == null)
        //    {
        //        return NotFound("No Labtests found");
        //    }

        //    return Ok(labtests);
        //}

        //#endregion

        //#region 3- get all labtest bill-search all
        //[HttpGet("vm1")]
        //public async Task<ActionResult<IEnumerable<LabtestBillViewModel>>> GetAllLabtestBillByViewModel()
        //{
        //    var labtests = await _repository.GetViewModelLabtestBill();
        //    if (labtests == null)
        //    {
        //        return NotFound("No Labtest Bill found");
        //    }

        //    return Ok(labtests);
        //}

        //#endregion

        //#region 4 -  get labtest report
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<LabTestReportViewModel>>> GetAllLabTestReports()

        //{
        //    var reports = await _repository.GetAllLabTestReportsAsync();
        //    return Ok(reports);
        //}
        //#endregion
        //#region 5 - Get a lab test report by ID
        //[HttpGet("GetLabTestReportById/{id}")]
        //public async Task<ActionResult<LabTestReportViewModel>> GetLabTestReportById(int id)
        //{
        //    var report = await _repository.GetLabTestReportByIdAsync(id);
        //    if (report == null) return NotFound();

        //    return Ok(report);
        //}
        //#endregion
        //#region 6 - Update a lab test report
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateLabTestReport(int id, LabTestReportViewModel model)
        //{
        //    if (id != model.TpId) return BadRequest("Report ID mismatch");

        //    var updated = await _repository.UpdateLabTestReportAsync(id, model);
        //    if (!updated) return NotFound();

        //    return NoContent();

        //}
        //#endregion

        //#region 7 - GetLabTestsForToday - Ahalya lab test

        //[HttpGet("GetLabTestsForToday")]
        //public async Task<IActionResult> GetLabTestsForToday()
        //{
        //    try
        //    {
        //        var labTests = await _repository.GetLabTestsForTodayAsync();

        //        if (labTests == null || !labTests.Any())
        //        {
        //            return NotFound(new { success = false, message = "No lab tests found for today." });
        //        }

        //        return Ok(new { success = true, data = labTests });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { success = false, message = ex.Message });
        //    }
        //}

        //#endregion

        //#region 8 - get all labtest

        //[HttpGet("today-prescribed-tests")]
        //public async Task<ActionResult<List<AppPatStaLabViewModel>>> GetTodaysPrescribedTests()
        //{
        //    try
        //    {
        //        var tests = await _repository.GetTodaysPrescribedLabTests();
        //        return Ok(tests);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Internal server error: " + ex.Message);
        //    }
        //}

        //#endregion
    }
}
