using ClinicManagementSys.Model;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSys.Repository
{
    public interface IViewLabReportRepository
    {
        #region   2 - Get an Patient based on Id
        public Task<ActionResult<LabTestReport>> GetLabTestReportById(int id);
        #endregion
    }
}
