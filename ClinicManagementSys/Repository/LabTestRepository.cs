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

                //Return the added Patient record
                return updateMedicineDetail;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        public async Task<IEnumerable<LabAppViewModel>> GetLabTestsForTodayAsync()
        {
            try
            {
                var today = DateTime.Today;

                var labTests = await (from tp in _context.TestPrescriptions
                                      join a in _context.Appointments on tp.AppointmentId equals a.AppointmentId
                                      join p in _context.Patients on a.PatientId equals p.PatientId
                                      join d in _context.Doctors on a.DoctorId equals d.DoctorId
                                      join lt in _context.Labtests on tp.LabTestId equals lt.LabTestId
                                      join lr in _context.LoginRegistrations on d.RegistrationId equals lr.RegistrationId
                                      join s in _context.Staff on lr.StaffId equals s.StaffId
                                      where a.AppointmentDate == today
                                      select new LabAppViewModel
                                      {
                                          LTReportId = tp.TpId,
                                          AppointmentId = a.AppointmentId,
                                          StaffId = s.StaffId,
                                          StaffName = s.StaffName,
                                          PatientId = p.PatientId,
                                          PatientName = p.PatientName,
                                          DoctorId = d.DoctorId,
                                          LabTestId = lt.LabTestId,
                                          HighRange = lt.HighRange.HasValue ? (int)lt.HighRange : 0,
                                          LowRange = lt.LowRange.HasValue ? (int)lt.LowRange : 0,
                                          ActualResult = 0, // Placeholder for lab test result logic
                                          Remarks = string.Empty // Placeholder for remarks
                                      }).ToListAsync();

                return labTests;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching lab tests for today.", ex);
            }
        }

    }

}

