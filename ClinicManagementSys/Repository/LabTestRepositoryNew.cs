using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSys.Repository
{
    public class LabTestRepositoryNew : ILabTestRepositoryNew
    {
        private readonly ClinicManagementSysContext _context;

        public LabTestRepositoryNew(ClinicManagementSysContext context)
        {
            _context = context;
        }

        public async Task<List<AppPatStaLabViewModel>> GetTodaysPrescribedLabTests()
        {
            var today = DateTime.Today;

            var prescribedTests = await (from tp in _context.TestPrescriptions
                                         join apt in _context.Appointments on tp.AppointmentId equals apt.AppointmentId
                                         join pat in _context.Patients on apt.PatientId equals pat.PatientId
                                         join doc in _context.Doctors on apt.DoctorId equals doc.DoctorId
                                         join reg in _context.LoginRegistrations on doc.RegistrationId equals reg.RegistrationId
                                         join staff in _context.Staff on reg.StaffId equals staff.StaffId
                                         join lab in _context.Labtests on tp.LabTestId equals lab.LabTestId
                                         where apt.AppointmentDate.Date == today
                                         select new AppPatStaLabViewModel
                                         {
                                             AppointmentId = apt.AppointmentId,
                                             PatientId = pat.PatientId,
                                             DoctorId = doc.DoctorId,
                                             RegistrationId = reg.RegistrationId,
                                             BloodGroup = pat.BloodGroup,
                                             StaffId = staff.StaffId,
                                             StaffName = staff.StaffName,
                                             LabTestId = lab.LabTestId,
                                             PatientName = pat.PatientName,
                                             DOB = pat.Dob,
                                             Gender = pat.Gender,
                                             PatientPhone = pat.PatientPhone,
                                             TestName = lab.TestName,
                                             TpId = tp.TpId,
                                             SampleItem = tp.SampleItem,
                                             TokenNumber = apt.TokenNumber
                                         }).ToListAsync();

            return prescribedTests;
        }

        public async Task<LabTestReport> CreateLabTestReport(LabTestReport report)
        {
            if (report == null)
                throw new ArgumentNullException(nameof(report), "Lab test report cannot be null");

            _context.LabTestReports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Labtest> GetLabTestDetails(int labTestId)
        {
            return await _context.Labtests
                .FirstOrDefaultAsync(l => l.LabTestId == labTestId);
        }

        public async Task<LabTestReportViewModel> GetReportDetailsByReportId(int reportId)
        {
            return await (from ltr in _context.LabTestReports
                          join apt in _context.Appointments on ltr.AppointmentId equals apt.AppointmentId
                          join pat in _context.Patients on apt.PatientId equals pat.PatientId
                          join lab in _context.Labtests on ltr.LabTestId equals lab.LabTestId
                          where ltr.LtreportId == reportId
                          select new LabTestReportViewModel
                          {
                              TpId = 0, // Not relevant when fetching by report ID
                              LtreportId = ltr.LtreportId,
                              AppointmentId = apt.AppointmentId,
                              LabTestId = lab.LabTestId,
                              HighRange = lab.HighRange,
                              LowRange = lab.LowRange,
                              ActualResult = ltr.ActualResult,
                              Remarks = ltr.Remarks,
                              PatientName = pat.PatientName,
                              BloodGroup = pat.BloodGroup,
                              Gender = pat.Gender,
                              PatientPhone = pat.PatientPhone,
                              TestName = lab.TestName,
                              SampleItem = lab.Sample
                          }).FirstOrDefaultAsync();
        }

        public async Task<LabtestBillViewModel> GetBillDetails(int reportId)
        {
            try
            {
                // Validate report exists with all necessary joins
                var billDetails = await (from ltr in _context.LabTestReports
                                         join apt in _context.Appointments on ltr.AppointmentId equals apt.AppointmentId
                                         join pat in _context.Patients on apt.PatientId equals pat.PatientId
                                         join lt in _context.Labtests on ltr.LabTestId equals lt.LabTestId
                                         where ltr.LtreportId == reportId
                                         select new LabtestBillViewModel
                                         {
                                             BillId = reportId, // Temporary, as bill table is not shown
                                             LtreportId = ltr.LtreportId,
                                             PatientName = pat.PatientName,
                                             PatientPhone = pat.PatientPhone,
                                             TestName = lt.TestName,
                                             Price = lt.Price ?? 0,
                                             Date = DateTime.Now
                                         }).FirstOrDefaultAsync();

                if (billDetails == null)
                    throw new Exception($"No details found for report ID {reportId}");

                return billDetails;
            }
            catch (Exception ex)
            {
                // Log exception details
                Console.WriteLine($"Error in GetBillDetails: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                throw;
            }
        }

    }
}