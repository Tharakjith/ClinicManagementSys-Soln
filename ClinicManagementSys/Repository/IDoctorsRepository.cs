using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
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
        public  Task<bool> AddDoctorAvailabilityAsync(DoctorAvailabilityViewModel model);
        public Task<DoctorViewModel> GetDoctorDetailsByPhoneAsync(string phoneNumber);
    }
}
