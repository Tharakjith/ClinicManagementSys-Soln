using ClinicManagementSys.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSys.Repository
{
    public class MedicinePrescriptionRepository : IMedicinePrescriptionRepository
    {

        private readonly ClinicManagementSysContext _context;

        public MedicinePrescriptionRepository(ClinicManagementSysContext context)
        {
            _context = context;
        }

        #region 1 -  Get all Medicine from DB 
        public async Task<ActionResult<IEnumerable<Prescription>>> GetMedicineDetail()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Prescriptions.ToListAsync();
                }
                //Returns an empty list if context is null
                return new List<Prescription>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region----------------name

        public async Task<ActionResult<IEnumerable<MedicineDetail>>> GetMedicineName()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.MedicineDetails.ToListAsync();
                }

                //return an empty list if context is null
                return new List<MedicineDetail>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


        #region   2 - Get an Patient based on Id

        public async Task<ActionResult<Prescription>> GetMedicineDetailById(int id)
        {
            try
            {
                if (_context != null)
                {
                    var patient = await _context.Prescriptions.FirstOrDefaultAsync(p => p.PrescriptionId == id);
                    return patient;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        /*   public async Task<List<object>> GetMedicineNamesAsync()
       {
           if (_context == null)
           {
               throw new InvalidOperationException("Database context is null.");
           }

           // LINQ Query to Retrieve MedicineId and MedicineName
           var medicineNames = await (from medicine in _context.MedicineDetails
                                      where medicine.IsActive // Filter for active medicines
                                      select new
                                      {
                                          MedicineId = medicine.MedicineId,
                                          MedicineName = medicine.MedicineName
                                      }).ToListAsync();

           // Cast the result to List<object> and return
           return medicineNames.Cast<object>().ToList();
       }

       * */

        #region  3  - Insert an Patient -return Patient record
        public async Task<ActionResult<Prescription>> PostMedicineDetailReturnRecord(Prescription patient)
{
    try
    {
        //check if patient object is not null
        if (patient == null)
        {
            throw new ArgumentException(nameof(patient), "MedicineDetail data is null");
        }
        //Ensure the context is not null
        if (_context == null)
        {
            throw new InvalidOperationException("Database context is not initialized");
        }

        //Add the Patient record to the DBContext
        await _context.Prescriptions.AddAsync(patient);

        //save changes to the database
        await _context.SaveChangesAsync();

        //Retrieve the Patient detail
        var newpatient = await _context.Prescriptions.FirstOrDefaultAsync(p => p.PrescriptionId == patient.PrescriptionId);

        //Return the added Patient with the record added
        return newpatient;
    }

    catch (Exception ex)
    {
        return null;
    }
}
#endregion

#region  4  - Update/Edit an Prescription with ID
public async Task<ActionResult<Prescription>> PutMedicineDetail(int id, Prescription patient)
{
    try
    {
        if (patient == null)
        {
            throw new InvalidOperationException("Database context is not initialized");
        }
        //Find the patient by id
        var existingMedicineDetail = await _context.Prescriptions.FindAsync(id);
        if (existingMedicineDetail == null)
        {
            return null;
        }

        //Map values wit fields
        existingMedicineDetail.AppointmentId = patient.AppointmentId;
        existingMedicineDetail.MedicineId = patient.MedicineId;

        //save changes to the database
        await _context.SaveChangesAsync();

                //Retreive the patient 
                var updateMedicineDetail = await _context.Prescriptions
                 //  .FirstOrDefaultAsync(existingMedicineDetail => existingMedicineDetail.PrescriptionId == Prescription.PrescriptionId);
                 .FirstOrDefaultAsync(existingPatient => existingMedicineDetail.PrescriptionId == patient.PrescriptionId);

        //Return the added Patient record
        return updateMedicineDetail;
    }
    catch (Exception ex)
    {
        return null;
    }
}
#endregion

#region 5  - Delete an Patient by id
public JsonResult DeleteMedicineDetail(int id)
{
    try
    {
        if (id <= null)
        {
            return new JsonResult(new
            {
                success = false,
                message = "Invalid PrescriptionId Id"
            })
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        //Ensure the context is not null
        if (_context == null)
        {
            return new JsonResult(new
            {
                success = false,
                message = "Database context is not initialized"
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        //Find the Patient by id
        var existingMedicineDetail = _context.Prescriptions.Find(id);

        if (existingMedicineDetail == null)
        {
            return new JsonResult(new
            {
                success = false,
                message = "Patient not found"
            })
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
        //Remove the Patient record from the DBContext
        _context.Prescriptions.Remove(existingMedicineDetail);

        //save changes to the database
        _context.SaveChanges();
        return new JsonResult(new
        {
            success = true,
            message = "Patient detail Deleted successfully"
        })
        {
            StatusCode = StatusCodes.Status200OK
        };
    }
    catch (Exception ex)
    {
        return new JsonResult(new
        {
            success = false,
            message = "Database context is not initialized"
        })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}
#endregion


    }
}