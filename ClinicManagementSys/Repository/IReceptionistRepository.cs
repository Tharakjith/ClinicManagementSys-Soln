using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSys.Repository
{
    public interface IReceptionistRepository
    {
        #region PATIENT REGISTRATION

        #region 1 -  Get all patients from DB 
        public Task<ActionResult<IEnumerable<Patient>>> GetPatients();
        #endregion

        #region   2 - Get an Patient based on Id
        public Task<ActionResult<Patient>> GetPatientById(int id);
        #endregion

        #region  3 -  Get Patient by phone number
        public Task<ActionResult<Patient>> GetPatientByPhoneNumber(string phoneNumber);
        #endregion

        #region  4  - Insert an Patient -return Patient record
        public Task<ActionResult<Patient>> PostPatientReturnRecord(Patient patient);
        #endregion

        #region  5  - Update/Edit an Patient with ID
        public Task<ActionResult<Patient>> PutPatient(int id, Patient patient);
        #endregion

        #region 6  - Delete an Patient by id
        public JsonResult DeletePatient(int id); //return type > JsonResult -> true/false
        #endregion

        #endregion

        #region APPOINTMENT BOOKING

        #region 1 -  Get all Appointments from DB 
        public Task<ActionResult<IEnumerable<Appointment>>> GetAppointments();
        #endregion

        #region 2 -  Get all Specialization from DB 
        public Task<ActionResult<IEnumerable<Specialization>>> GetSpecializations();
        #endregion

        #region 3 -  Get all doctors from DB 
        public Task<ActionResult<IEnumerable<Doctor>>> GetDoctors();
        #endregion

        #region  4 - Get all Doctors based on Specialization
        public Task<IEnumerable<object>> GetDoctorsBySpecializationWithStaffDetails(int specializationId);
        #endregion

        #region 5 - Get Consultation Fee by Doctor ID
        public Task<decimal> GetConsultationFeeByDoctorId(int doctorId);
        #endregion

        #region 6 - Get Daily Availability of a Doctor by DoctorId
        public Task<IEnumerable<object>> GetDoctorAvailability(int doctorId);
        #endregion

        #region 7 - Auto-generate Token Number for Appointments
        public Task<int> GenerateTokenNumber(int doctorId, DateTime appointmentDate, int timeSlotId);
        #endregion

        #region 8 - Insert Appointment and return success
        public Task<ActionResult<Appointment>> BookAppointment(Appointment appointment);

        #endregion

        #region 9 - Patient Bill generate View model
        public Task<PatientBillVm?> GetPatientBillByIdAsync(int patientId);
        #endregion

        #endregion
    }
}