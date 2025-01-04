using ClinicManagementSys.Model;
using ClinicManagementSys.Repository;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace ClinicManagementSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewReportLabtestController : Controller
    {
        private readonly IViewLabReportRepository _repository;

        public ViewReportLabtestController(IViewLabReportRepository startDiagnosysRepository)
        {
            _repository = startDiagnosysRepository;
        }
        #region 2 - Get an LabTestReport based on Id
        [HttpGet("{id}")]
        public async Task<ActionResult<LabTestReport>> GetAllLabTestReportById(int id)
        {
            var patient = await _repository.GetLabTestReportById(id);
            if (patient == null)
            {
                return NotFound("Oops! No Patient found on this Id");
            }
            return Ok(patient);
        }
        #endregion
    }
}
