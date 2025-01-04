using ClinicManagementSys.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ClinicManagementSys.Repository.LabTestRepository;

namespace ClinicManagementSys.Repository
{
    public class LabTestRepository: ILabTestRepository
    {

        private readonly ClinicManagementSysContext _context;

        public LabTestRepository(ClinicManagementSysContext context)
        {
            _context = context;
        }
        #region 1 -  Get all Medicine from DB 
        public async Task<ActionResult<IEnumerable<TestPrescription>>> GetMedicineDetail()
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
        public async Task<ActionResult<TestPrescription>> GetMedicineDetailById(int id)
        {
            try
            {
                if (_context != null)
                {
                    var patient = await _context.TestPrescriptions.FirstOrDefaultAsync(p => p.TpId == id);
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
        #region  4  - Update/Edit an Prescription with ID
        public async Task<ActionResult<LabTestReport>> PutMedicineDetail(int id, TestPrescription patient)
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
                existingMedicineDetail.AppointmentId = patient.AppointmentId;
                existingMedicineDetail.TpId = patient.TpId;

                //save changes to the database
                await _context.SaveChangesAsync();

                //Retreive the patient 
                var updateMedicineDetail = await _context.LabTestReports
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


    }

}

