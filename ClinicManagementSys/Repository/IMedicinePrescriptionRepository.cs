using ClinicManagementSys.Model;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSys.Repository
{
    public interface IMedicinePrescriptionRepository
    {
        #region 1 -  Get all patients from DB 
        public Task<ActionResult<IEnumerable<Prescription>>> GetMedicineDetail();
        #endregion

        #region   2 - Get all medicine name
        public Task<ActionResult<IEnumerable<MedicineDetail>>> GetMedicineName();

        #endregion

        #region  3  - Insert an Patient -return Patient record
        public Task<ActionResult<Prescription>> PostMedicineDetailReturnRecord(Prescription patient);
        #endregion

        #region  4  - Update/Edit an Patient with ID
        public Task<ActionResult<Prescription>> PutMedicineDetail(int id, Prescription patient);
        #endregion

        #region 5  - Delete an Patient by id
        public JsonResult DeleteMedicineDetail(int id); //return type > JsonResult -> true/false
        #endregion
    }
}