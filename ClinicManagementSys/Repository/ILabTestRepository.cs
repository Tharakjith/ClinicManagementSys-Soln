using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSys.Repository
{
    public interface ILabTestRepository
    {

        #region   2 - Get an Patient based on Id
        public Task<ActionResult<TestPrescription>> GetMedicineDetailById(int id);
        #endregion

        #region  4  - Update/Edit an Patient with ID
        public Task<ActionResult<LabTestReport>> PutMedicineDetail(int id, TestPrescription patient);
        #endregion

       

    }
}
