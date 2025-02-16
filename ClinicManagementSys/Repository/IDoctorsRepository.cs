using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using ClinicManagementSys.ViewModel.ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSys.Repository
{
    public interface IDoctorsRepository
    {
        #region 1-Get all Doctor
        public Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors();

        #endregion

        #region 2-Get Doctor by id 
        public Task<ActionResult<Doctor>> GetDoctorbycode(int id);
        #endregion

        #region 3- insert all records
        public Task<ActionResult<int>> insertDoctor(Doctor doctor);
        #endregion 

        #region  4-update Doctor by its id
        public Task<ActionResult<Doctor>> updateDoctor(int id, Doctor doctor);
        #endregion
        #region 7-delete Doctor
        public JsonResult Deletedoctor(int id);
        #endregion
        public Task<bool> AddDoctorAvailabilityAsync(DoctorAvailabilityViewModel model);
        public Task<DoctorViewModel> GetDoctorDetailsByPhoneAsync(string phoneNumber);
        public Task<ActionResult<Doctor>> postTblEmployeesReturnRecord(Doctor employee);
        public Task<ActionResult<IEnumerable<Specialization>>> GetTblDepartments();

        public Task<ActionResult<IEnumerable<LoginRegistration>>> GetTblUsers();

        public Task<ActionResult<IEnumerable<Staff>>> GetTblstaffs();
        public bool RegisterDoctor(doctorlistnew model);
        public Task<ActionResult<IEnumerable<Weekday>>> listallweekdays();
        public Task<IEnumerable<Timeslot>> listalltimeslotsrtments();

        //public Task<bool> InsertAvailabilityAsync(Availability availability);
        ////AVAILABILITY
        //public Task<Timeslot> GetTimeslotByIdAsync(int timeslotId);
        //public Task<Doctor> GetDoctorByIdAsync(int doctorId);
        //Task<Timeslot?> GetTimeslotByIdAsync(int timeslotId);

        ///////////////////////////////////////////////////////////////////////
        //NEW AVAILABILITY
        Task<object> AddAvailability(Availability availability);
        public Task<IEnumerable<Timeslot>> GetAllTimeslots();
        public Task<Timeslot> GetTimeSlotDetails(int timeSlotId);
    }

}