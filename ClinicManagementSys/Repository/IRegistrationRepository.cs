using ClinicManagementSys.Model;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSys.Repository
{
    public interface IRegistrationRepository
    {
        #region 1-Get all login
        public Task<ActionResult<IEnumerable<LoginRegistration>>> GetAlllogin();

        #endregion

        #region 2-Get login by id 
        public Task<ActionResult<LoginRegistration>> Getloginbycode(int id);
        #endregion

        #region 3- insert all records
        public Task<LoginRegistration> AddLoginRegistration(LoginRegistration loginRegistration);
        #endregion 

        #region  4-update login by its id
        public Task<ActionResult<LoginRegistration>> updatelogin(int id, LoginRegistration login);
        #endregion
        #region 7-delete login
        public JsonResult Deletelogin(int id);
        #endregion
        public Task<ActionResult<IEnumerable<Staff>>> GetTblDepartments();
        public Task<ActionResult<IEnumerable<Role>>> Getroles();
    }
}