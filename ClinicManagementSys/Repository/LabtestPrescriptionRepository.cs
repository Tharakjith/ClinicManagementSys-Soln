using ClinicManagementSys.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSys.Repository
{
    public class LabtestPrescriptionRepository : ILabtestPrescriptionRepository
    {

        private readonly ClinicManagementSysContext _context;

        public LabtestPrescriptionRepository(ClinicManagementSysContext context)
        {
            _context = context;
        }

        #region 1 -  Get all Medicine from DB 
        public async Task<ActionResult<IEnumerable<TestPrescription>>> GetTestPrescription()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.TestPrescriptions.ToListAsync();
                }
                //Returns an empty list if context is null
                return new List<TestPrescription>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region   2 - Get an Patient based on Id

        public async Task<ActionResult<LabTestReport>> GetReportDetailById(int id)
        {
            try
            {
                if (_context != null)
                {
                    var patient = await _context.LabTestReports.FirstOrDefaultAsync(p => p.AppointmentId == id);
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


        #region -----------LabtestName
        public async Task<ActionResult<IEnumerable<Labtest>>> GetLabtestName()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Labtests.ToListAsync();
                }

                //return an empty list if context is null
                return new List<Labtest>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  3  - Insert an Patient -return Patient record
        //public async Task<ActionResult<TestPrescription>> PostTestPrescriptionReturnRecord(TestPrescription patient)
        //{
        //    try
        //    {
        //        //check if patient object is not null
        //        if (patient == null)
        //        {
        //            throw new ArgumentException(nameof(patient), "TestPrescription data is null");
        //        }
        //        //Ensure the context is not null
        //        if (_context == null)
        //        {
        //            throw new InvalidOperationException("Database context is not initialized");
        //        }

        //        //Add the Patient record to the DBContext
        //        await _context.TestPrescriptions.AddAsync(patient);

        //        //save changes to the database
        //        await _context.SaveChangesAsync();

        //        //Retrieve the Patient detail
        //        var newpatient = await _context.TestPrescriptions.FirstOrDefaultAsync(p => p.AppointmentId == patient.TpId);

        //        //Return the added Patient with the record added
        //        return newpatient;
        //    }

        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        public async Task<ActionResult<TestPrescription>> PostTestPrescriptionReturnRecord(TestPrescription patient)
        {
            try
            {
                // Validate input
                if (patient == null)
                {
                    throw new ArgumentException(nameof(patient), "TestPrescription data is null");
                }

                // Ensure the context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                // Create a new TestPrescription object with only the allowed fields
                var newTestPrescription = new TestPrescription
                {
                    AppointmentId = patient.AppointmentId,
                    LabTestId = patient.LabTestId,
                    SampleItem = patient.SampleItem
                };

                // Add only the TestPrescription record to the DBContext
                await _context.TestPrescriptions.AddAsync(newTestPrescription);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Retrieve the added TestPrescription record
                var addedPrescription = await _context.TestPrescriptions
                    .FirstOrDefaultAsync(p => p.TpId == newTestPrescription.TpId);

                return addedPrescription;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region  4  - Update/Edit an Prescription with ID
        public async Task<ActionResult<TestPrescription>> PutTestPrescription(int id, TestPrescription patient)
        {
            try
            {
                if (patient == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                //Find the patient by id
                var existingMedicineDetail = await _context.TestPrescriptions.FindAsync(id);
                if (existingMedicineDetail == null)
                {
                    return null;
                }

                //Map values wit fields
                existingMedicineDetail.LabTestId = patient.LabTestId;
                existingMedicineDetail.AppointmentId = patient.AppointmentId;

                //save changes to the database
                await _context.SaveChangesAsync();

                //Retreive the patient 
                var updateMedicineDetail = await _context.TestPrescriptions
                 //  .FirstOrDefaultAsync(existingMedicineDetail => existingMedicineDetail.PrescriptionId == Prescription.PrescriptionId);
                 .FirstOrDefaultAsync(existingPatient => existingMedicineDetail.TpId == patient.TpId);

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
        public JsonResult DeleteTestPrescription(int id)
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
                var existingMedicineDetail = _context.TestPrescriptions.Find(id);

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
                _context.TestPrescriptions.Remove(existingMedicineDetail);

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

        public Task<List<object>> GetLabtestNameAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<LabTestReport>> GetTestPrescriptionById(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}