using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSys.Repository
{
    public class LabtestListRepository : ILabtestListRepository
    {
        //EF -VirtualDatabase
        private readonly ClinicManagementSysContext _context;

        //DI -- Constructor Injection
        public LabtestListRepository(ClinicManagementSysContext context)
        {
            _context = context;
        }
        #region 1- Get all using ViewModel
        public async Task<ActionResult<IEnumerable<AppPatStaLabViewModel>>> GetViewModelLabtestList()
        {
            //LINQ
            try
            {
                if (_context != null)
                {
                    /*
                     SELECT e.EnployeeId, e.EnployeeName, d.DepartnentName
                     FROM TbLEmpLoyees e
                     JOIN TDLDepartnents d
                     ON e.DepartmentId=d.DepartnentId
                    */
                    // LINQ
                    //return await db.TblEmployees.ToListAsync();
                    return await (from appointment in _context.Appointments
                                  join patient in _context.Patients
                                      on appointment.PatientId equals patient.PatientId
                                  join doctor in _context.Doctors
                                      on appointment.DoctorId equals doctor.DoctorId
                                  join staff in _context.Staff
                                      on doctor.RegistrationId equals staff.StaffId
                                  join testPrescription in _context.TestPrescriptions
                                      on appointment.AppointmentId equals testPrescription.AppointmentId
                                  join labTest in _context.Labtests
                                      on testPrescription.LabTestId equals labTest.LabTestId
                                  select new AppPatStaLabViewModel
                                  {
                                      TokenNumber = appointment.TokenNumber,
                                      PatientName = patient.PatientName,
                                      DOB = patient.Dob,
                                      Gender = patient.Gender,
                                      BloodGroup = patient.BloodGroup,
                                      StaffName = staff.StaffName,
                                      TestName = labTest.TestName
                                  }).ToListAsync();


                }
                //Return an empty List if context is null
                return new List<AppPatStaLabViewModel>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data: {ex.Message}");

            }
        }
        #endregion

        #region 2 - Search By Id
        public async Task<AppPatStaLabViewModel> GetLabtestById(int tokenNumber)
        {
            try
            {
                if (_context == null)
                    throw new Exception("Database context is unavailable.");

                var result = await (from appointment in _context.Appointments
                                    join patient in _context.Patients
                                        on appointment.PatientId equals patient.PatientId
                                    join doctor in _context.Doctors
                                        on appointment.DoctorId equals doctor.DoctorId
                                    join staff in _context.Staff
                                        on doctor.RegistrationId equals staff.StaffId
                                    join testPrescription in _context.TestPrescriptions
                                        on appointment.AppointmentId equals testPrescription.AppointmentId
                                    join labTest in _context.Labtests
                                        on testPrescription.LabTestId equals labTest.LabTestId
                                    where appointment.TokenNumber == tokenNumber
                                    select new AppPatStaLabViewModel
                                    {
                                        TokenNumber = appointment.TokenNumber,
                                        PatientName = patient.PatientName,
                                        DOB = patient.Dob,
                                        Gender = patient.Gender,
                                        BloodGroup = patient.BloodGroup,
                                        StaffName = staff.StaffName,
                                        TestName = labTest.TestName
                                    }).FirstOrDefaultAsync();

                if (result == null)
                    throw new KeyNotFoundException($"No record found for TokenNumber: {tokenNumber}");

                return result;
            }
            catch (KeyNotFoundException knfEx)
            {
                throw new Exception(knfEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data: {ex.Message}");
            }
        }
        #endregion

        //#region 3- Get all using ViewModel
        //public async Task<ActionResult<IEnumerable<EmpDeptViewModel>>> GetViewModelEmployees()
        //{
        //    //LINQ
        //    try
        //    {
        //        if (_context != null)
        //        {
        //            /*
        //             SELECT e.EnployeeId, e.EnployeeName, d.DepartnentName
        //             FROM TbLEmpLoyees e
        //             JOIN TDLDepartnents d
        //             ON e.DepartmentId=d.DepartnentId
        //            */
        //            // LINQ
        //            //return await db.TblEmployees.ToListAsync();
        //            return await (from e in _context.TblEmployees
        //                          from d in _context.TblDepartments
        //                          where e.DepartmentId == d.DepartmentId
        //                          select new EmpDeptViewModel
        //                          {
        //                              EmployeeId = e.EmployeeId,
        //                              EmployeeName = e.EmployeeName,
        //                              Designation = e.Designation,
        //                              DepartmentName = d.DepartmentName,
        //                              Contact = e.Contact
        //                          }).ToListAsync();
        //        }
        //        //Return an empty List if context is null
        //        return new List<EmpDeptViewModel>();
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //#endregion

        #region 4 - get labtest report
        public async Task<IEnumerable<LabTestReportViewModel>> GetAllLabTestReportsAsync()
        {
            return await _context.TestPrescriptions
                .Select(report => new LabTestReportViewModel
                {
                    TpId = report.TpId,
                    AppointmentId = report.AppointmentId ?? 0,
                    // PatientId = report.PatientId ?? 0,
                    LabTestId = report.LabTestId ?? 0,
                    TestName = report.LabTest.TestName,
                    HighRange = report.LabTest.LabTestReports.FirstOrDefault().HighRange ?? 0,
                    LowRange = report.LabTest.LabTestReports.FirstOrDefault().LowRange ?? 0,
                    ActualResult = report.LabTest.LabTestReports.FirstOrDefault().ActualResult ?? 0,
                    Remarks = report.LabTest.LabTestReports.FirstOrDefault().Remarks
                }).ToListAsync();
        }
        #endregion

        #region 5 - Get a lab test report by ID
        public async Task<LabTestReportViewModel> GetLabTestReportByIdAsync(int id)
        {
            return await _context.LabTestReports
                .Where(report => report.LtreportId == id)
                .Select(report => new LabTestReportViewModel
                {
                    TpId = report.LtreportId,
                    AppointmentId = report.AppointmentId,
                    PatientId = report.Appointment.PatientId,
                    LabTestId = report.LabTestId,
                    TestName = report.LabTest.TestName,
                    HighRange = report.HighRange ?? 0,
                    LowRange = report.LowRange ?? 0,
                    ActualResult = report.ActualResult ?? 0,
                    Remarks = report.Remarks
                }).FirstOrDefaultAsync();
        }
        #endregion

        #region 6 - Update a lab test report
        public async Task<bool> UpdateLabTestReportAsync(int id, LabTestReportViewModel model)
        {
            var report = await _context.LabTestReports.FindAsync(id);
            if (report == null) return false;

            report.HighRange = model.HighRange;
            report.LowRange = model.LowRange;
            report.ActualResult = model.ActualResult;
            report.Remarks = model.Remarks;

            _context.LabTestReports.Update(report);
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

    }
}
