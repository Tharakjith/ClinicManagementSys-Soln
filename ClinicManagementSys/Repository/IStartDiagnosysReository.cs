using ClinicManagementSys.Model;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSys.Repository
{
    public interface IStartDiagnosysReository
    {

        #region 1 -  Get all patients from DB 
        public Task<ActionResult<IEnumerable<StartDiagnosy>>> GetStartDiagnosy();
        #endregion

        #region   2 - Get an Patient based on Id
        public Task<ActionResult<StartDiagnosy>> GetStartDiagnosyById(int id);
        #endregion

        #region  3  - Insert an Patient -return Patient record
        public Task<ActionResult<StartDiagnosy>> PostStartDiagnosyReturnRecord(StartDiagnosy patient);
        #endregion
        #region 3 -  Get all doctors from DB 
        public Task<ActionResult<IEnumerable<Doctor>>> GetDoctors();
        #endregion

        #region  4  - Update/Edit an Patient with ID
        public Task<ActionResult<StartDiagnosy>> PutStartDiagnosy(int id, StartDiagnosy patient);
        #endregion

        #region 5  - Delete an Patient by id
        public JsonResult DeleteStartDiagnosy(int id); //return type > JsonResult -> true/false
        #endregion
    }
}
