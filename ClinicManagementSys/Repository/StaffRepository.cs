using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSys.Repository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ClinicManagementSysContext _context;

        public StaffRepository(ClinicManagementSysContext context)
        {
            _context = context;
        }

        public JsonResult Deletestaff(int id)
        {
            try
            {//check idf employee object is not null
                if (id <= 0)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid staff id"

                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };

                }
                // ensure context is not null
                if (_context == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Database coontext is not initialized"

                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
                //Find the employee by id
                var existingstaff = _context.Staff.Find(id);
                if (existingstaff == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Staff  not found"

                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                //remove

                _context.Staff.Remove(existingstaff);



                //save changes to the database
                _context.SaveChanges();



                return new JsonResult(new
                {
                    success = true,
                    message = "Staff deleted successfully"

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
                    message = "Database coontext is not initialized"

                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

        }

        public async Task<ActionResult<IEnumerable<Staff>>> GetAllStaffs()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Staff.Include(s => s.Department).OrderByDescending(s => s.StaffId).ToListAsync();
                }


                return new List<Staff>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<Staff>> Getstaffbycode(int id)
        {
            try
            {
                if (_context != null)
                {
                    // find the employee by id 
                    var st = await _context.Staff
                    .Include(st => st.Department)
                    .FirstOrDefaultAsync(e => e.StaffId == id);
                    return st;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }





        public async Task<ActionResult<IEnumerable<StaffDepViewModel>>> GetstaffDetals()
        {
            try
            {
                if (_context != null)
                {
                    //linq
                    return await (from e in _context.Staff
                                  from d in _context.Departments
                                  where e.DepartmentId == d.DepartmentId
                                  select new StaffDepViewModel
                                  {

                                      StaffName = e.StaffName,
                                      Address = e.Address,
                                      PhoneNumber = e.PhoneNumber,
                                      StaffIsActive = e.StaffIsActive,
                                      StaffId = e.StaffId,
                                      DepartmentName = d.DepartmentName
                                  }).ToListAsync();


                }

                //return an empty list if context is null
                return new List<StaffDepViewModel>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<int>> insertstaffs(Staff staff)
        {
            try
            {//check idf staff object is not null
                if (staff == null)
                {
                    throw new ArgumentNullException(nameof(staff), "staff data is null");

                }
                // ensure context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }
                //add the employee record to the dbcontext
                await _context.Staff.AddAsync(staff);

                //save changes to the database
                var changesRecord = await _context.SaveChangesAsync();
                if (changesRecord > 0)
                {
                    return staff.StaffId;
                }
                else
                {
                    throw new Exception("failed to save staff record to the database");
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<Staff>> updatestaff(int id, Staff staff)
        {
            try
            {//check  staff object is not null
                if (staff == null)
                {
                    throw new ArgumentNullException(nameof(staff), "staff data is null");

                }
                // ensure context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }
                //Find the employee by id
                var existingstaff = await _context.Staff.FindAsync(id);
                if (existingstaff == null)
                {
                    return null;
                }

                //map values with fields - update
                existingstaff.Address = staff.Address;
                existingstaff.PhoneNumber = staff.PhoneNumber;
                existingstaff.Email = staff.Email;


                existingstaff.StaffIsActive = staff.StaffIsActive;




                //save changes to the database
                await _context.SaveChangesAsync();
                //retrive the employee with the related departement
                var staffs = await _context.Staff.Include(e => e.Department)
                    .FirstOrDefaultAsync(e => e.StaffId == staff.StaffId);
                return staffs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<StaffDepViewModel> GetStaffDetailsAsync(int? staffId, string phoneNumber)
        {
            var query = _context.Staff.Include(s => s.Department)
                .Where(s => (staffId.HasValue || s.StaffId == staffId.Value) ||
                            (!string.IsNullOrEmpty(phoneNumber) & s.PhoneNumber == phoneNumber))
                .Select(s => new StaffDepViewModel
                {
                    StaffName = s.StaffName,
                    StaffId = s.StaffId,
                    Dob = s.Dob,
                    Address = s.Address,
                    PhoneNumber = s.PhoneNumber,
                    Email = s.Email,
                    Gender = s.Gender,
                    StaffIsActive = s.StaffIsActive,
                    DepartmentName = s.Department.DepartmentName
                });

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ActionResult<IEnumerable<Department>>> GetTblDepartments()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Departments.ToListAsync();
                }

                //return an empty list if context is null
                return new List<Department>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ActionResult<Staff>> postTblEmployeesReturnRecord(Staff employee)
        {
            try
            {
                if (employee == null)
                {
                    throw new ArgumentNullException(nameof(employee), "Employee data is null");
                }

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }

                // Validate DepartmentId
                if (!await _context.Departments.AnyAsync(d => d.DepartmentId == employee.DepartmentId))
                {
                    throw new InvalidOperationException("Invalid DepartmentId");
                }

                // Add employee to DbContext
                await _context.Staff.AddAsync(employee);
                await _context.SaveChangesAsync();

                // Retrieve the employee with the related department
                var employeewithDepartment = await _context.Staff.Include(e => e.Department)
                    .FirstOrDefaultAsync(e => e.StaffId == employee.StaffId);

                return employeewithDepartment;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new BadRequestObjectResult($"An error occurred: {ex.Message}");
            }
        }

    }
}