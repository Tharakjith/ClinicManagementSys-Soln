using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ClinicManagementSys.Repository.LabTestRepository;

namespace ClinicManagementSys.Repository
{
    public class LabTestRepository: ILabTestRepository
    {

        private readonly ClinicManagementSysContext _context;

        public LabTestRepository(ClinicManagementSysContext context)
        {
            _context = context;
        }
        #region 1 -  Get all Medicine from DB 
        public async Task<ActionResult<IEnumerable<TestPrescription>>> GetMedicineDetail()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.TestPrescriptions.ToListAsync();
                }
                //Returns an empty list if context is null
                return new List<TestPrescription>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region   2 - Get an Patient based on Id
        public async Task<ActionResult<TestPrescription>> GetMedicineDetailById(int id)
        {
            try
            {
                if (_context != null)
                {
                    var patient = await _context.TestPrescriptions.FirstOrDefaultAsync(p => p.TpId == id);
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
        #region  4  - Update/Edit an Prescription with ID
        public async Task<ActionResult<LabTestReport>> PutMedicineDetail(int id, TestPrescription patient)
        {
            try
            {
                if (patient == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                //Find the patient by id
                var existingMedicineDetail = await _context.TestPrescriptions.FindAsync(id);
                if (existingMedicineDetail == null)
                {
                    return null;
                }

                //Map values wit fields
                existingMedicineDetail.AppointmentId = patient.AppointmentId;
                existingMedicineDetail.TpId = patient.TpId;

                //save changes to the database
                await _context.SaveChangesAsync();

                //Retreive the patient 
                var updateMedicineDetail = await _context.LabTestReports
                 //  .FirstOrDefaultAsync(existingMedicineDetail => existingMedicineDetail.PrescriptionId == Prescription.PrescriptionId);
                 .FirstOrDefaultAsync(existingPatient => existingMedicineDetail.TpId == patient.TpId);

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

