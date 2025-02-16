using ClinicManagementSys.Model;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSys.Repository
{
    public interface ILabtestPrescriptionRepository
    {
        #region 1 -  Get all TestPrescription from DB 
        public Task<ActionResult<IEnumerable<TestPrescription>>> GetTestPrescription();
        #endregion


        #region   2 - Get an Patient based on Id
        public Task<ActionResult<IEnumerable<Labtest>>> GetLabtestName();

        #endregion

        public Task<ActionResult<LabTestReport>> GetReportDetailById(int id);

        #region   2 - Get an Patient based on Id
        // public Task<List<object>> GetLabtestNameAsync();
        #endregion

        #region   2 - Get an TestPrescription based on Id
        public Task<ActionResult<LabTestReport>> GetTestPrescriptionById(int id);
        #endregion

        #region  3  - Insert an TestPrescription -return Patient record
        public Task<ActionResult<TestPrescription>> PostTestPrescriptionReturnRecord(TestPrescription patient);
        #endregion

        #region  4  - Update/Edit an TestPrescription with ID
        public Task<ActionResult<TestPrescription>> PutTestPrescription(int id, TestPrescription patient);
        #endregion

        #region 5  - Delete an TestPrescription by id
        public JsonResult DeleteTestPrescription(int id); //return type > JsonResult -> true/false
        #endregion
    }
}
