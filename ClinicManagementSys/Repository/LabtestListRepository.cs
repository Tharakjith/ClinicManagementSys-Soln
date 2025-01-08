using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSys.Repository
{
    public class LabtestListRepository : ILabtestListRepository
    {
        //EF -VirtualDatabase
        private readonly ClinicManagementSysContext _context;

        //DI -- Constructor Injection
        public LabtestListRepository(ClinicManagementSysContext context)
        {
            _context = context;
        }
        #region 1- Get all using ViewModel
        public async Task<ActionResult<IEnumerable<AppPatStaLabViewModel>>> GetViewModelLabtestList()
        {
            //LINQ
            try
            {
                if (_context != null)
                {
                    /*
                     SELECT e.EnployeeId, e.EnployeeName, d.DepartnentName
                     FROM TbLEmpLoyees e
                     JOIN TDLDepartnents d
                     ON e.DepartmentId=d.DepartnentId
                    */
                    // LINQ
                    //return await db.TblEmployees.ToListAsync();
                    return await (from appointment in _context.Appointments
                                  join patient in _context.Patients
                                      on appointment.PatientId equals patient.PatientId
                                  join doctor in _context.Doctors
                                      on appointment.DoctorId equals doctor.DoctorId
                                  join staff in _context.Staff
                                      on doctor.RegistrationId equals staff.StaffId
                                  join testPrescription in _context.TestPrescriptions
                                      on appointment.AppointmentId equals testPrescription.AppointmentId
                                  join labTest in _context.Labtests
                                      on testPrescription.LabTestId equals labTest.LabTestId
                                  select new AppPatStaLabViewModel
                                  {
                                      TokenNumber = appointment.TokenNumber,
                                      PatientName = patient.PatientName,
                                      DOB = patient.Dob,
                                      Gender = patient.Gender,
                                      BloodGroup = patient.BloodGroup,
                                      StaffName = staff.StaffName,
                                      TestName = labTest.TestName
                                  }).ToListAsync();


                }
                //Return an empty List if context is null
                return new List<AppPatStaLabViewModel>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data: {ex.Message}");

            }
        }
        #endregion

        #region 2 - Search By Id
        public async Task<AppPatStaLabViewModel> GetLabtestById(int tokenNumber)
        {
            try
            {
                if (_context == null)
                    throw new Exception("Database context is unavailable.");

                var result = await (from appointment in _context.Appointments
                                    join patient in _context.Patients
                                        on appointment.PatientId equals patient.PatientId
                                    join doctor in _context.Doctors
                                        on appointment.DoctorId equals doctor.DoctorId
                                    join staff in _context.Staff
                                        on doctor.RegistrationId equals staff.StaffId
                                    join testPrescription in _context.TestPrescriptions
                                        on appointment.AppointmentId equals testPrescription.AppointmentId
                                    join labTest in _context.Labtests
                                        on testPrescription.LabTestId equals labTest.LabTestId
                                    where appointment.TokenNumber == tokenNumber
                                    select new AppPatStaLabViewModel
                                    {
                                        TokenNumber = appointment.TokenNumber,
                                        PatientName = patient.PatientName,
                                        DOB = patient.Dob,
                                        Gender = patient.Gender,
                                        BloodGroup = patient.BloodGroup,
                                        StaffName = staff.StaffName,
                                        TestName = labTest.TestName
                                    }).FirstOrDefaultAsync();

                if (result == null)
                    throw new KeyNotFoundException($"No record found for TokenNumber: {tokenNumber}");

                return result;
            }
            catch (KeyNotFoundException knfEx)
            {
                throw new Exception(knfEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data: {ex.Message}");
            }
        }
        #endregion

        #region 3- Get all using ViewModel
        public async Task<ActionResult<IEnumerable<LabtestBillViewModel>>> GetViewModelLabtestBill()
        {
            //LINQ
            try
            {
                if (_context != null)
                {
                    return await (from p in _context.Patients
                                  from s in _context.Staff
                                  from l in _context.Labtests
                                  select new LabtestBillViewModel
                                  {
                                      PatientName = p.PatientName,
                                      DoctorName = s.StaffName, // assuming staff are acting as doctors
                                      TestName = l.TestName,
                                      Price = l.Price
                                  }).ToListAsync();
                }
                //Return an empty List if context is null
                return new List<LabtestBillViewModel>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        //#region 4 - Update an Employee with ID and employee

        //public async Task<ActionResult<TblEmployee>> PutTblEmployee(int id, TblEmployee employee)
        //{
        //    try
        //    {
        //        //check if employee object is not null
        //        if (employee == null)
        //        {
        //            throw new ArgumentNullException(nameof(employee), "Employee data is null");
        //            //return null;
        //        }
        //        //ensure the context is not null
        //        if (_context == null)
        //        {
        //            throw new InvalidOperationException("Database context is not initialized");
        //        }

        //        //Find the employee by ID
        //        var existingEmployee = await _context.TblEmployees.FindAsync(id);

        //        if (existingEmployee == null)
        //        {
        //            return null;
        //        }

        //        //map values with fields - Update
        //        //Existing Employee data  newdata
        //        existingEmployee.EmployeeName = employee.EmployeeName;
        //        existingEmployee.Designation = employee.Designation;
        //        existingEmployee.DateOfJoining = employee.DateOfJoining;
        //        existingEmployee.DepartmentId = employee.DepartmentId;
        //        existingEmployee.Contact = employee.Contact;
        //        existingEmployee.IsActive = employee.IsActive;


        //        //save changes to the database
        //        await _context.SaveChangesAsync();

        //        //Retrieve the employee with the related Department
        //        var employeeWithDepartment = await _context.TblEmployees
        //            .Include(e => e.Department)//Eager load
        //            .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

        //        //Return the added employee record
        //        return employeeWithDepartment;

        //    }
        //    catch (Exception ex)
        //    {
        //        //Log Exception here if needed
        //        //throw new Exception($"An error occured while inserting the employeee record: {ex.Message}",
        //        return null;
        //    }

        //}
        //#endregion

        //#region 5 - Insert an Employee- Return Employee Record
        //public async Task<ActionResult<TblEmployee>> PostTblEmployeesReturnRecord(TblEmployee employee)
        //{
        //    try
        //    {
        //        //check if employee object is not null
        //        if (employee == null)
        //        {
        //            throw new ArgumentNullException(nameof(employee), "Employee data is null");
        //            //return null;
        //        }
        //        //ensure the context is not null
        //        if (_context == null)
        //        {
        //            throw new InvalidOperationException("Database context is not initialized");
        //        }

        //        //Add the employee record to the DbContext
        //        await _context.TblEmployees.AddAsync(employee);

        //        //save changes to the database
        //        await _context.SaveChangesAsync();

        //        //Retrieve the employee with the related Department
        //        var employeeWithDepartment = await _context.TblEmployees
        //            .Include(e => e.Department)//Eager load
        //            .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

        //        //Return the added employee record
        //        return employeeWithDepartment;
        //    }
        //    catch (Exception ex)
        //    {
        //        //log exception here if needed
        //        return null;
        //    }
        //}

    }
}
