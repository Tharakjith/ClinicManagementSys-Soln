using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ClinicManagementSys.Repository.ILabTestRepository;
using static ClinicManagementSys.Repository.LabTestRepository;

namespace ClinicManagementSys.Repository
{
    public class LabTestRepository : ILabTestRepository
    {

        //private readonly ClinicManagementSysContext _context;

        //public LabTestRepository(ClinicManagementSysContext context)
        //{
        //    _context = context;
        //}
        //public async Task<IEnumerable<LabTestReportViewModel>> GetAllLabTestReportsAsync()
        //{
        //    return await _context.TestPrescriptions
        //        .Select(report => new LabTestReportViewModel
        //        {
        //            TpId = report.TpId,
        //            AppointmentId = report.AppointmentId ?? 0,
        //           // PatientId = report.PatientId ?? 0,
        //            LabTestId = report.LabTestId ?? 0,
        //            TestName = report.LabTest.TestName,
        //            HighRange = report.LabTest.LabTestReports.FirstOrDefault().HighRange ?? 0,
        //            LowRange = report.LabTest.LabTestReports.FirstOrDefault().LowRange ?? 0,
        //            ActualResult = report.LabTest.LabTestReports.FirstOrDefault().ActualResult ?? 0,
        //            Remarks = report.LabTest.LabTestReports.FirstOrDefault().Remarks
        //        }).ToListAsync();
        //}

        //public async Task<LabTestReportViewModel> GetLabTestReportByIdAsync(int id)
        //{
        //    return await _context.LabTestReports
        //        .Where(report => report.LtreportId == id)
        //        .Select(report => new LabTestReportViewModel
        //        {
        //            TpId = report.LtreportId,
        //            AppointmentId = report.AppointmentId,
        //            PatientId = report.Appointment.PatientId,
        //            LabTestId = report.LabTestId,
        //            TestName = report.LabTest.TestName,
        //            HighRange = report.HighRange ?? 0,
        //            LowRange = report.LowRange ?? 0,
        //            ActualResult = report.ActualResult ?? 0,
        //            Remarks = report.Remarks
        //        }).FirstOrDefaultAsync();
        //}

        //public async Task<bool> UpdateLabTestReportAsync(int id, LabTestReportViewModel model)
        //{
        //    var report = await _context.LabTestReports.FindAsync(id);
        //    if (report == null) return false;

        //    report.HighRange = model.HighRange;
        //    report.LowRange = model.LowRange;
        //    report.ActualResult = model.ActualResult;
        //    report.Remarks = model.Remarks;

        //    _context.LabTestReports.Update(report);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

    }

}

