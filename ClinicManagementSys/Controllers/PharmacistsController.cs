using ClinicManagementSys.Model;
using ClinicManagementSys.Repository;
using ClinicManagementSys.ViewModel;
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
        #region 2- get all Prescription Bill-search all
        [HttpGet("vm2")]
        public async Task<ActionResult<IEnumerable<PrescriptionBillViewModel>>> GetAllMedicineBillByViewModel()
        {
            var medicinebill = await _repository.GetViewModelPrescriptionBill();
            if (medicinebill == null)
            {
                return NotFound("No Medicine Bill found");
            }

            return Ok(medicinebill);
        }
        #endregion
    }

}
