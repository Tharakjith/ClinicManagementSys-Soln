using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSys.Repository
{
    public class PharmacistRepository :IPharmacistRepository
    {
        //EF -VirtualDatabase
        private readonly ClinicManagementSysContext _context;

        //DI -- Constructor Injection
        public PharmacistRepository(ClinicManagementSysContext context)
        {
            _context = context;
        }

        #region 1 - Get All Medicines - Search All
        public async Task<ActionResult<IEnumerable<MedicineDetail>>> GetTblMedicines()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.MedicineDetails.Include(ctg => ctg.Category).ToListAsync();
                }
                //Return an empty List if context is null
                return new List<MedicineDetail>();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

        #region 2 - Search By Id
        public async Task<ActionResult<MedicineDetail>> GetTblMedicineById(int id)
        {
            try
            {
                if (_context != null)
                {
                    var Medicine = await _context.MedicineDetails
                    .Include(emp => emp.Category).FirstOrDefaultAsync(emp => emp.MedicineId == id);
                    return Medicine;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region 3 - Insert an Medicine- Return Medicine Record
        public async Task<ActionResult<MedicineDetail>> PostMedicineReturnRecord(MedicineDetail medicine)
        {
            try
            {
                //check if medicine object is not null
                if (medicine == null)
                {
                    throw new ArgumentNullException(nameof(medicine), "Medicine data is null");
                    //return null;
                }
                //ensure the context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                //Add the medicine record to the DbContext
                await _context.MedicineDetails.AddAsync(medicine);

                //save changes to the database
                await _context.SaveChangesAsync();

                //Retrieve the employee with the related Department
                var medicineWithCategory = await _context.MedicineDetails
                    .Include(e => e.Category)//Eager load
                    .FirstOrDefaultAsync(e => e.MedicineId == medicine.MedicineId);

                //Return the added employee record
                return medicineWithCategory;
            }
            catch (Exception ex)
            {
                //log exception here if needed
                return null;
            }
        }



        #endregion

        #region 4 - Update an Medicine with ID and Medicine Name

        public async Task<ActionResult<MedicineDetail>> PutTblMedicine(int id, MedicineDetail medicine)
        {
            try
            {
                //check if medicine object is not null
                if (medicine == null)
                {
                    throw new ArgumentNullException(nameof(medicine), "Medicine data is null");
                    //return null;
                }
                //ensure the context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                //Find the employee by ID
                var existingMedicine = await _context.MedicineDetails.FindAsync(id);

                if (existingMedicine == null)
                {
                    return null;
                }

                //map values with fields - Update
                //Existing Medicine data  newdata
                existingMedicine.MedicineName = medicine.MedicineName;
                existingMedicine.ManufacturingDate = medicine.ManufacturingDate;
                existingMedicine.ExpiryDate = medicine.ExpiryDate;
                existingMedicine.CategoryId = medicine.CategoryId;
                existingMedicine.Cost = medicine.Cost;
                existingMedicine.IsActive = medicine.IsActive;


                //save changes to the database
                await _context.SaveChangesAsync();

                //Retrieve the employee with the related Department
                var medicineWithCategory = await _context.MedicineDetails
                    .Include(e => e.Category)//Eager load
                    .FirstOrDefaultAsync(e => e.MedicineId == medicine.MedicineId);

                //Return the added employee record
                return medicineWithCategory;

            }
            catch (Exception ex)
            {
                //Log Exception here if needed
                //throw new Exception($"An error occured while inserting the employeee record: {ex.Message}",
                return null;
            }

        }
        #endregion

        #region 5 - Delete an Medicine

        public JsonResult DeleteTblMedicine(int id)
        {
            try
            {

                //check if medicine object is not null
                if (id == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "invalid Medicine id"

                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };

                }

                //ensure the context is not null

                if (_context == null)
                {

                    return new JsonResult(new
                    {

                        success = false,
                        message = "Database context is not initialized. "

                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }

                //find the medicine by ID
                var existingMedicine = _context.MedicineDetails.Find(id);

                if (existingMedicine == null)
                {
                    return new JsonResult(new
                    {

                        success = false,
                        message = "Medicine Not Found . "

                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                //remove the medicine record from the database

                _context.MedicineDetails.Remove(existingMedicine);

                //save changes to the database

                _context.SaveChanges();

                return new JsonResult(new
                {

                    success = false,
                    message = "Medicine Deleted Successfully . "

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
                    message = "An error accured "

                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        #endregion

        #region 6- Get all using ViewModel
        public async Task<ActionResult<IEnumerable<PrescriptionViewModel>>> GetViewModelPrescription()
        {
            // LINQ
            try
            {
                if (_context != null)
                {
                    var prescriptions = await (from p in _context.Prescriptions
                                               join a in _context.Appointments on p.AppointmentId equals a.AppointmentId
                                               join pt in _context.Patients on a.PatientId equals pt.PatientId
                                               join d in _context.Doctors on a.DoctorId equals d.DoctorId
                                               join lr in _context.LoginRegistrations on d.RegistrationId equals lr.RegistrationId
                                               join s in _context.Staff on lr.StaffId equals s.StaffId
                                               join m in _context.MedicineDetails on p.MedicineId equals m.MedicineId
                                               where a.AppointmentDate.Date == DateTime.Today // Filter by today's date in the background
                                               select new PrescriptionViewModel
                                               {
                                                   PrescriptionId = p.PrescriptionId,
                                                   AppointmentId = p.AppointmentId,
                                                   MedicineId = p.MedicineId,
                                                   MedicineName = m.MedicineName,
                                                   Dosage = p.Dosage,
                                                   Frequency = p.Frequency,
                                                   NumberofDays = p.NumberofDays,
                                                   PatientName = pt.PatientName,
                                                   StaffName = s.StaffName
                                               }).ToListAsync();

                    return prescriptions;
                }

                return new List<PrescriptionViewModel>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 7- Get all bill using ViewModel
        public async Task<PrescriptionBillViewModel?> GetBillDetailsByPrescriptionIdAsync(int prescriptionId)
        {
            var billDetails = await (from prescription in _context.Prescriptions
                                     join appointment in _context.Appointments on prescription.AppointmentId equals appointment.AppointmentId
                                     join patient in _context.Patients on appointment.PatientId equals patient.PatientId
                                     join medicine in _context.MedicineDetails on prescription.MedicineId equals medicine.MedicineId
                                     where prescription.PrescriptionId == prescriptionId
                                     select new PrescriptionBillViewModel
                                     {
                                         PrescriptionId = prescription.PrescriptionId,
                                         AppointmentId = appointment.AppointmentId,
                                         PatientId = patient.PatientId,
                                         PatientName = patient.PatientName,
                                         MedicineId = medicine.MedicineId,
                                         MedicineName = medicine.MedicineName,
                                         BillDateTime = DateTime.Now, // or fetch from a related bill table if available
                                         Dosage = prescription.Dosage,
                                         Frequency = prescription.Frequency,
                                         NumberOfDays = prescription.NumberofDays,
                                         Cost = medicine.Cost
                                     }).FirstOrDefaultAsync();

            return billDetails;
        }
    


#endregion

#region 8- Medicine distribute viewmodel
public async Task<List<MedicineDistributionViewModel>> GetViewModelMedicineDitribute()
        {
            //LINQ
            try
            {
                if (_context != null)
                {

                    return await (from md in _context.MedicineDistributions
                                  join p in _context.Prescriptions on md.PrescriptionId equals p.PrescriptionId
                                  join mi in _context.MedicineInventories on md.MedicineId equals mi.MedicineId
                                  join ms in _context.MedDistributionStatuses on md.MedStatusId equals ms.MedStatusId
                                  join mdet in _context.MedicineDetails on md.MedicineId equals mdet.MedicineId
                                  select new MedicineDistributionViewModel
                                  {
                                      DistributionId = md.MedDistId,
                                      PrescriptionId = md.PrescriptionId,
                                      MedicineId = md.MedicineId,
                                      QuantityDistributed = md.QuantityDistributed,
                                      DistributionDate = md.DistributionDate,
                                      MedStatusId = md.MedStatusId,
                                      MedicineName = mdet.MedicineName,
                                      Dosage = p.Dosage,
                                      Frequency = p.Frequency,
                                      NumberofDays = p.NumberofDays,
                                      StockInHand = mi.StockInHand, 
                                      MedStatusName = ms.MedStatusName
                                  }).ToListAsync();
                }
                //Return an empty List if context is null
                return new List<MedicineDistributionViewModel>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 9- Get all categories

        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            try
            {
                if (_context != null)
                {
                    //find th Category by id
                    return await _context.Categories.ToListAsync();
                }
                //Return an empty List if context is null
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 10 - Get All Medicine prescription details by view model - Search All
        public async Task<ActionResult<IEnumerable<MedicineDistribution>>> GetTblMedicineDistribution()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.MedicineDistributions.ToListAsync();
                }
                //Return an empty List if context is null
                return new List<MedicineDistribution>();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

        #region 11 - Insert an Medicine prescription details- Return Prescription Record
        public async Task<ActionResult<MedicineDistribution>> PostTblMedicinePrescriptionReturnRecord(MedicineDistribution medicineDistribution)
        {
            try
            {
                //check if employee object is not null
                if (medicineDistribution == null)
                {
                    throw new ArgumentNullException(nameof(medicineDistribution), "medicinedistribution data is null");
                    //return null;
                }
                //ensure the context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                //Add the employee record to the DbContext
                await _context.MedicineDistributions.AddAsync(medicineDistribution);

                //save changes to the database
                await _context.SaveChangesAsync();

                return medicineDistribution;
            }
            catch (Exception ex)
            {
                //log exception here if needed
                return null;
            }
        }
        #endregion


        #region 12- post all Medicine prescription using ViewModel
        public async Task<bool> AddMedicineDistributionAsync(MedicineDistribution model)
        {
            if (_context == null || model == null)
            {
                return false;
            }

            try
            {
                // Create a new MedicineDistribution entity from the ViewModel
                var medicineDistribution = new MedicineDistribution
                {
                    MedDistId = model.MedDistId,
                    PrescriptionId = model.PrescriptionId,
                    MedicineId = model.MedicineId,
                    QuantityDistributed = model.QuantityDistributed,
                    DistributionDate = DateTime.Now, // Assuming the current date and time for distribution
                    MedStatusId = model.MedStatusId
                };

                // Add the new entity to the context
                await _context.MedicineDistributions.AddAsync(medicineDistribution);

                // Save changes to the database
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Handle exception (you can log it here)
                return false;
            }
        }

        #endregion

        #region 13 - 

        public async Task<IEnumerable<LabAppViewModel>> GetLabTestsForTodayAsync()
        {
            try
            {
                var today = DateTime.Today;

                var labTests = await (from tp in _context.TestPrescriptions
                                      join a in _context.Appointments on tp.AppointmentId equals a.AppointmentId
                                      join p in _context.Patients on a.PatientId equals p.PatientId
                                      join d in _context.Doctors on a.DoctorId equals d.DoctorId
                                      join lt in _context.Labtests on tp.LabTestId equals lt.LabTestId
                                      join lr in _context.LoginRegistrations on d.RegistrationId equals lr.RegistrationId
                                      join s in _context.Staff on lr.StaffId equals s.StaffId
                                      where a.AppointmentDate == today
                                      select new LabAppViewModel
                                      {
                                          LTReportId = tp.TpId,
                                          AppointmentId = a.AppointmentId,
                                          StaffId = s.StaffId,
                                          StaffName = s.StaffName,
                                          PatientId = p.PatientId,
                                          PatientName = p.PatientName,
                                          DoctorId = d.DoctorId,
                                          LabTestId = lt.LabTestId,
                                          HighRange = lt.HighRange.HasValue ? (int)lt.HighRange : 0,
                                          LowRange = lt.LowRange.HasValue ? (int)lt.LowRange : 0,
                                          ActualResult = 0, // Placeholder for lab test result logic
                                          Remarks = string.Empty // Placeholder for remarks
                                      }).ToListAsync();

                return labTests;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching lab tests for today.", ex);
            }
        }
        #endregion

        #region 14 - Stock Management

        //medicine quantity reduction
        public async Task<ActionResult<MedicineInventory>> GetMedicineInventoryByMedicineIdAsync(int medicineId)
        {
            try
            {
                var inventory = await _context.MedicineInventories
                    .FirstOrDefaultAsync(mi => mi.MedicineId == medicineId);

                return inventory;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateMedicineInventoryQuantityAsync(int medicineId, int quantityDistributed)
        {
            try
            {
                var inventory = await _context.MedicineInventories
                    .FirstOrDefaultAsync(mi => mi.MedicineId == medicineId);

                if (inventory == null || inventory.StockInHand < quantityDistributed)
                {
                    return false;
                }

                // Update stock and issuance
                inventory.StockInHand -= quantityDistributed;
                inventory.Issuance += quantityDistributed;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ActionResult<MedicineDistribution>> DistributeMedicineWithInventoryUpdateAsync(MedicineDistribution medicineDistribution)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Validate input
                if (medicineDistribution == null || !medicineDistribution.MedicineId.HasValue)
                {
                    return null;
                }

                // Check inventory
                var inventory = await GetMedicineInventoryByMedicineIdAsync(medicineDistribution.MedicineId.Value);
                if (inventory?.Value == null || inventory.Value.StockInHand < medicineDistribution.QuantityDistributed)
                {
                    return null;
                }

                // Update inventory
                bool inventoryUpdated = await UpdateMedicineInventoryQuantityAsync(
                    medicineDistribution.MedicineId.Value,
                    medicineDistribution.QuantityDistributed);

                if (!inventoryUpdated)
                {
                    return null;
                }

                // Set distribution date if not set
                if (!medicineDistribution.DistributionDate.HasValue)
                {
                    medicineDistribution.DistributionDate = DateTime.Now;
                }

                // Add distribution record
                await _context.MedicineDistributions.AddAsync(medicineDistribution);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return medicineDistribution;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return null;
            }
        }
        #endregion
    }
}
