using ClinicManagementSys.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSys.Repository
{
    public class ViewLabReportRepository: IViewLabReportRepository
    {
        private readonly ClinicManagementSysContext _context;

        public ViewLabReportRepository(ClinicManagementSysContext context)
        {
            _context = context;
        }

        #region   2 - Get an Patient based on Id
        public async Task<ActionResult<LabTestReport>> GetLabTestReportById(int id)
        {
            try
            {
                if (_context != null)
                {
                    var patient = await _context.LabTestReports.FirstOrDefaultAsync(p => p.AppointmentId == id);
                    return patient;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
