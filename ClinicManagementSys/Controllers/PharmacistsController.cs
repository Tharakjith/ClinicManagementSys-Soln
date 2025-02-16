using ClinicManagementSys.Model;
using ClinicManagementSys.Repository;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacistsController : ControllerBase
    {
        //Call repository
        private readonly IPharmacistRepository _repository;

        //DI Constructor Injection
        public PharmacistsController(IPharmacistRepository repository)
        {
            _repository = repository;
        }

        #region 1- get all Medicine-search all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicineDetail>>> GetAllMedicines()

        {
            var medicines = await _repository.GetTblMedicines();
            if (medicines == null)
            {
                return NotFound("No Medicine found");
            }

            return Ok(medicines);
        }

        #endregion

        #region 2- get all medicines-search by id
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicineDetail>> GetMedicinesById(int id)
        {
            var medicines = await _repository.GetTblMedicineById(id);
            if (medicines == null)
            {
                return NotFound("No medicines found");
            }

            return Ok(medicines);
        }

        #endregion

        #region 3 -  Insert an medicines - Return medicines Record
        [HttpPost]
        public async Task<ActionResult<MedicineDetail>> PostMedicineReturnRecord(MedicineDetail medicine)
        {
            if (ModelState.IsValid)
            {
                //insert a new record and return as an object named employee
                var newMedicine = await _repository.PostMedicineReturnRecord(medicine);
                if (newMedicine != null)
                {
                    return Ok(newMedicine);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region 4 - Update Medicine - Return  MedicineRecord
        [HttpPut("{id}")]

        public async Task<ActionResult<MedicineDetail>> UpdatePutTblEmployee(int id, MedicineDetail medicine)
        {
            if (ModelState.IsValid)
            {
                //insert a new record and return as an object named employee
                var updateMedicine = await _repository.PutTblMedicine(id, medicine);
                if (updateMedicine != null)
                {
                    return Ok(updateMedicine);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();

        }

        #endregion

        #region 5 - Delete Medicine
        [HttpDelete("{id}")]
        public IActionResult DeleteMedicine(int id)
        {
            try
            {
                var result = _repository.DeleteTblMedicine(id);
                if (result == null)
                {
                    //If result indicates failure or null
                    return NotFound(new
                    {
                        success = false,
                        message = "Medicine could not be deleted or not found"
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                //Log exception in real-world scenarios
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexpected error occured" });
            }
        }
        #endregion

        #region 6- get all Prescriptions-search all
        [HttpGet("vm1")]
        public async Task<ActionResult<IEnumerable<PrescriptionViewModel>>> GetAllPrescriptionByViewModel()
        {
            var prescriptions = await _repository.GetViewModelPrescription();
            if (prescriptions == null)
            {
                return NotFound("No Prescription found");
            }

            return Ok(prescriptions);
        }

        #endregion
        #region 7 - get all Prescription Bill-search all
        [HttpGet("GetBillDetails/{prescriptionId}")]
        public async Task<IActionResult> GetBillDetails(int prescriptionId)
        {
            var billDetails = await _repository.GetBillDetailsByPrescriptionIdAsync(prescriptionId);

            if (billDetails == null)
            {
                return NotFound(new { Message = "Bill details not found for the given PrescriptionId." });
            }

            return Ok(billDetails);
        }
        #endregion

        #region 8 - get all Medicine DIstribute by view model
        [HttpGet("vm3")]
        public async Task<ActionResult<IEnumerable<MedicineDistributionViewModel>>> GetAllMedicineDistributeByViewModel()
        {
            var medicinedistribute = await _repository.GetViewModelMedicineDitribute();
            if (medicinedistribute == null)
            {
                return NotFound("No Medicine to Distribute is not found");
            }

            return Ok(medicinedistribute);
        }
        #endregion

        #region 9 Get All Categories
        [HttpGet("v2")] 
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategory()
        {
            var category = await _repository.GetCategory();
            if (category == null)
            {
                return NotFound("No Categories found");
            }

            return Ok(category);
        }
        #endregion

        #region 10 - Get All Medicine prescription details by view model - Search All
        [HttpGet("v3")]

        public async Task<ActionResult<IEnumerable<MedicineDistribution>>> GetAllMedicineDistribution()
        {
            var medicineDistribution = await _repository.GetTblMedicineDistribution();
            if (medicineDistribution == null)
            {
                return NotFound("No Medicine prescription found");
            }

            return Ok(medicineDistribution);
        }

        #endregion

        #region 11 - Insert an Medicine prescription details- Return Prescription Record
        [HttpPost("md")]
        public async Task<ActionResult<MedicineDistribution>> InsertPostTblMedicineDistributionReturnRecord(MedicineDistribution medicineDistribution)
        {
            if (ModelState.IsValid)
            {
                //insert a new record and return as an object named employee
                var newMedicineDistribution = await _repository.PostTblMedicinePrescriptionReturnRecord(medicineDistribution);
                if (newMedicineDistribution != null)
                {
                    return Ok(newMedicineDistribution);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region 12- post all Medicine prescription using ViewModel
        [HttpPost("add-md")]
        public async Task<ActionResult> AddMedicineDistribution([FromBody] MedicineDistribution model)
        {
            if (model == null)
            {
                return BadRequest("Invalid data.");
            }

            var result = await _repository.AddMedicineDistributionAsync(model);

            if (!result)
            {
                return StatusCode(500, "An error occurred while adding the medicine distribution.");
            }

            return Ok("Medicine distribution added successfully.");
        }


        #endregion


        #region 13 - Stock management

        //quantity reduction
        [HttpGet("inventory/{medicineId}")]
        public async Task<ActionResult<MedicineInventory>> GetMedicineInventory(int medicineId)
        {
            var inventory = await _repository.GetMedicineInventoryByMedicineIdAsync(medicineId);

            if (inventory == null || inventory.Value == null)
            {
                return NotFound("Inventory not found for the specified medicine");
            }

            return Ok(inventory.Value);
        }

        [HttpPost("distribute")]
        public async Task<ActionResult<MedicineDistribution>> DistributeMedicine(MedicineDistribution medicineDistribution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _repository.DistributeMedicineWithInventoryUpdateAsync(medicineDistribution);

            if (result == null)
            {
                return BadRequest(new
                {
                    message = "Failed to distribute medicine. Please check inventory availability."
                });
            }

            return Ok(result);
        }
        #endregion

    }

}
