using ClinicManagementSys.Model;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSys.Repository
{
    public interface ILabTestRepository
    {
        #region 1 -  Get all patients from DB 
        public Task<ActionResult<IEnumerable<TestPrescription>>> GetMedicineDetail();
        #endregion

        #region   2 - Get an Patient based on Id
        public Task<ActionResult<TestPrescription>> GetMedicineDetailById(int id);
        #endregion

        #region  4  - Update/Edit an Patient with ID
        public Task<ActionResult<LabTestReport>> PutMedicineDetail(int id, TestPrescription patient);
        #endregion



    }
}
