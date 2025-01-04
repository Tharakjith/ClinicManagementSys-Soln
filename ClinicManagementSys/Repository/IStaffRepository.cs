
using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSys.Repository
{
    public interface IStaffRepository
    {
        #region 1-Get all staffs
        public Task<ActionResult<IEnumerable<Staff>>> GetAllStaffs();

        #endregion
        
        #region 2-Get staffs by id 
        public Task<ActionResult<Staff>> Getstaffbycode(int id);
        #endregion
        
        #region 3- insert all records
        public Task<ActionResult<int>> insertstaffs(Staff staff);
        #endregion 

        #region  4-update Staff by its id
        public Task<ActionResult<Staff>> updatestaff(int id, Staff staff);
        #endregion
        #region 5-delete Staff
        public JsonResult Deletestaff(int id);
        #endregion
        public  Task<ActionResult<IEnumerable<StaffDepViewModel>>> GetstaffDetals();
        public  Task<StaffDepViewModel> GetStaffDetailsAsync(int? staffId, string phoneNumber);
    }
}

