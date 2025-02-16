using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

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
        public Task<PrescriptionBillViewModel?> GetBillDetailsByPrescriptionIdAsync(int prescriptionId);

        //8 - Get All Medicine Distribute details using viewmodel
        public Task<List<MedicineDistributionViewModel>> GetViewModelMedicineDitribute();

        //9 - Get all categories
        public Task<ActionResult<IEnumerable<Category>>> GetCategory();

        //10 - Get All Medicine prescription details by view model - Search All
        public Task<ActionResult<IEnumerable<MedicineDistribution>>> GetTblMedicineDistribution();

        //11 - Insert an Medicine prescription details- Return Prescription Record
        public Task<ActionResult<MedicineDistribution>> PostTblMedicinePrescriptionReturnRecord(MedicineDistribution medicineDistribution);

        //12- post all Medicine prescription using ViewModel
        public Task<bool> AddMedicineDistributionAsync(MedicineDistribution model);

        //13- Stock Management
        //quantity reduction
        public Task<bool> UpdateMedicineInventoryQuantityAsync(int medicineId, int quantityDistributed);
        public Task<ActionResult<MedicineDistribution>> DistributeMedicineWithInventoryUpdateAsync(MedicineDistribution medicineDistribution);
        public Task<ActionResult<MedicineInventory>> GetMedicineInventoryByMedicineIdAsync(int medicineId);
    }
}
