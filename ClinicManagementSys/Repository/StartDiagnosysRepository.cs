using ClinicManagementSys.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSys.Repository
{
    public class StartDiagnosysRepository : IStartDiagnosysReository
    {
        private readonly ClinicManagementSysContext _context;

        public StartDiagnosysRepository(ClinicManagementSysContext context)
        {
            _context = context;
        }

        #region 1 -  Get all Medicine from DB 
        public async Task<ActionResult<IEnumerable<StartDiagnosy>>> GetStartDiagnosy()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.StartDiagnosys.ToListAsync();
                }
                //Returns an empty list if context is null
                return new List<StartDiagnosy>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region   2 - Get an Patient based on Id
        public async Task<ActionResult<StartDiagnosy>> GetStartDiagnosyById(int id)
        {
            try
            {
                if (_context != null)
                {
                    var patient = await _context.StartDiagnosys.FirstOrDefaultAsync(p => p.HistoryId == id);
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

        #region  3  - Insert an Patient -return Patient record
        public async Task<ActionResult<StartDiagnosy>> PostStartDiagnosyReturnRecord(StartDiagnosy patient)
        {
            try
            {
                //check if patient object is not null
                if (patient == null)
                {
                    throw new ArgumentException(nameof(patient), "StartDiagnosy data is null");
                }
                //Ensure the context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                //Add the Patient record to the DBContext
                await _context.StartDiagnosys.AddAsync(patient);

                //save changes to the database
                await _context.SaveChangesAsync();

                //Retrieve the Patient detail
                var newpatient = await _context.StartDiagnosys.FirstOrDefaultAsync(p => p.AppointmentId == patient.AppointmentId);

                //Return the added Patient with the record added
                return newpatient;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region 3 -  Get all doctors  name from DB 
        public async Task<List<Object>> GetDoctorNamesAsync()
        {
            if (_context == null)
            {
                throw new InvalidOperationException("Database context is null.");
            }

            // LINQ Query to Retrieve DoctorId and StaffName
            var doctorNames = await (from doctor in _context.Doctors
                                     join login in _context.LoginRegistrations
                                         on doctor.RegistrationId equals login.RegistrationId
                                     join staff in _context.Staff
                                         on login.StaffId equals staff.StaffId
                                    // where doctor.DoctorIsActive // Filter for active doctors
                                     select new
                                     {
                                         DoctorId = doctor.DoctorId,
                                         DoctorName = staff.StaffName
                                     }).ToListAsync();

            return doctorNames.Cast<Object>().ToList();
        }


        #endregion

        #region  4  - Update/Edit an Prescription with ID
        public async Task<ActionResult<StartDiagnosy>> PutStartDiagnosy(int id, StartDiagnosy patient)
        {
            try
            {
                if (patient == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                //Find the patient by id
                var existingMedicineDetail = await _context.StartDiagnosys.FindAsync(id);
                if (existingMedicineDetail == null)
                {
                    return null;
                }

                //Map values wit fields
                existingMedicineDetail.AppointmentId = patient.AppointmentId;
                //existingMedicineDetail.MedicineId = patient.MedicineId;

                //save changes to the database
                await _context.SaveChangesAsync();

                //Retreive the patient 
                var updateMedicineDetail = await _context.StartDiagnosys
                 //  .FirstOrDefaultAsync(existingMedicineDetail => existingMedicineDetail.PrescriptionId == Prescription.PrescriptionId);
                 .FirstOrDefaultAsync(existingPatient => existingMedicineDetail.HistoryId == patient.HistoryId);

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
        public JsonResult DeleteStartDiagnosy(int id)
        {
            try
            {
                if (id <= null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid StartDiagnosy Id"
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
                var existingMedicineDetail = _context.StartDiagnosys.Find(id);

                if (existingMedicineDetail == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "History not found"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                //Remove the Patient record from the DBContext
                _context.StartDiagnosys.Remove(existingMedicineDetail);

                //save changes to the database
                _context.SaveChanges();
                return new JsonResult(new
                {
                    success = true,
                    message = "Diagnosys detail Deleted successfully"
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
