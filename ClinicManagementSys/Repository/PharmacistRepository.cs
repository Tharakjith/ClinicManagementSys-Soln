﻿using ClinicManagementSys.Model;
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
            //LINQ
            try
            {
                if (_context != null)
                {

                    return await (from p in _context.Prescriptions
                                  from a in _context.Appointments
                                  from pt in _context.Patients
                                  from d in _context.Doctors
                                  from s in _context.Staff
                                  where p.AppointmentId == a.AppointmentId
                                        && a.PatientId == pt.PatientId
                                        && a.DoctorId == d.DoctorId
                                        && s.StaffId == s.StaffId
                                  select new PrescriptionViewModel
                                  {
                                      PrescriptionId = p.PrescriptionId,
                                      AppointmentId = p.AppointmentId,
                                      MedicineId = p.MedicineId,
                                      Dosage = p.Dosage,
                                      Frequency = p.Frequency,
                                      NumberofDays = p.NumberofDays,
                                      PatientName = pt.PatientName,
                                      StaffName = s.StaffName // Assuming Doctor has a foreign key to Staff for StaffName
                                  }).ToListAsync();

                }
                //Return an empty List if context is null
                return new List<PrescriptionViewModel>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 7- Get all bill using ViewModel
        public async Task<ActionResult<IEnumerable<PrescriptionBillViewModel>>> GetViewModelPrescriptionBill()
        {
            //LINQ
            try
            {
                if (_context != null)
                {
                    return await (from pr in _context.Prescriptions
                                  from a in _context.Appointments
                                  from pt in _context.Patients
                                  from md in _context.MedicineDetails
                                  where pr.AppointmentId == a.AppointmentId
                                        && a.PatientId == pt.PatientId
                                        && pr.MedicineId == md.MedicineId
                                  select new PrescriptionBillViewModel
                                  {
                                      PatientId = pt.PatientId,
                                      PatientName = pt.PatientName,
                                      BillDateTime = DateTime.Now, // Automatically set to system date and time
                                      MedicineName = md.MedicineName,
                                      Dosage = pr.Dosage,
                                      Frequency = pr.Frequency,
                                      NumberOfDays = pr.NumberofDays,
                                      Cost = md.Cost
                                  }).ToListAsync();

                }
                //Return an empty List if context is null
                return new List<PrescriptionBillViewModel>();
            }
            catch (Exception ex)
            {
                return null;
            }
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
                    //find th employee by id
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

    }
}
