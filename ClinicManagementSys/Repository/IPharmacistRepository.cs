using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSys.Repository
{
    public interface IPharmacistRepository
    {
        //Get all medicines from DB
        //Search All
        public Task<ActionResult<IEnumerable<MedicineDetail>>> GetTblMedicines();

        //2 -Get an Medicine based on Id
        public Task<ActionResult<MedicineDetail>> GetTblMedicineById(int id);

        //3 - Insert an medicine - RetuRN Medicine Record
        public Task<ActionResult<MedicineDetail>> PostMedicineReturnRecord(MedicineDetail medicine);

        //4 - Update an Medicine with ID and medicine
        public Task<ActionResult<MedicineDetail>> PutTblMedicine(int id, MedicineDetail medicine);

        //5 - Delete an Medicine
        public JsonResult DeleteTblMedicine(int id);

        //6 - Get all Prescriptions from DB
        public Task<ActionResult<IEnumerable<PrescriptionViewModel>>> GetViewModelPrescription();

        //7- Get All PrescriptionBill using ViewModel;
        public Task<ActionResult<IEnumerable<PrescriptionBillViewModel>>> GetViewModelPrescriptionBill();
    }
}
