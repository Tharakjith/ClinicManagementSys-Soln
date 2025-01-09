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

        #region Insert - Appointment Booking
        //public Task<ActionResult<Appointment>> PostBookAppointment(Appointment appointment);
        #endregion

        #region  4 - Get all Doctors based on Specialization
        public Task<ActionResult<IEnumerable<Doctor>>> GetDoctorsBySpecialization(int specializationId);
        #endregion

        #region 5 - Get Daily Availability of a Doctor
       // public Task<ActionResult<IEnumerable<DailyAvailability>>> GetDoctorDailyAvailability(int doctorId, DateTime date);
        #endregion

        #region 6 - Get Doctor's Daily Availability by Doctor ID and Date
        public Task<Weekday> GetWeekdayByName(string dayName);

        public Task<IEnumerable<Availability>> GetAvailabilityByDoctorIdAndWeekday(int doctorId, int weekdayId);

        public Task<ActionResult<IEnumerable<Availability>>> GetDoctorAvailabilityByDoctorIdAndDate(int doctorId, DateTime date);
        #endregion

        #region 7 - Get Consultation Fee by Doctor ID
        public Task<decimal> GetConsultationFeeByDoctorId(int doctorId);
        #endregion

        #region 8 - Auto-generate Token Number for Appointments
        Task<int> GenerateTokenNumber(int doctorId, DateTime appointmentDate);
        #endregion

        #region 9 - Insert Appointment and return success
        Task<ActionResult<Appointment>> BookAppointment(Appointment appointment);
        #endregion

        #endregion
    }
}
