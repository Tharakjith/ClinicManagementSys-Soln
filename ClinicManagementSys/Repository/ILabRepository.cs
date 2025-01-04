using ClinicManagementSys.Model;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSys.Repository
{
    public interface ILabRepository
    {
        #region 1-Get all tests 

        public Task<ActionResult<IEnumerable<Labtest>>> GetAllLabtest();

        #endregion

        #region 2-Get Labtest by id 
        public Task<ActionResult<Labtest>> GetLabtestbycode(int id);
        #endregion

        #region 3- insert all records
        public Task<ActionResult<int>> insertLabtest(Labtest labtests);
        #endregion 

        #region  4-update Labtest by its id
        public Task<ActionResult<Labtest>> updateLabtestf(int id, Labtest labtests);
        #endregion
        #region 7-delete Labtest
        public JsonResult Deletelabtests(int id);  
        #endregion

    }
}
