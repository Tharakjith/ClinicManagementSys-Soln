using ClinicManagementSys.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace ClinicManagementSys.Repository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly ClinicManagementSysContext _context;

        public RegistrationRepository(ClinicManagementSysContext context)
        {
            _context = context;
        }
        public JsonResult Deletelogin(int id)
        {
            try
            {
                // Check if the ID is valid
                if (id <= 0)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid login ID"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                // Ensure context is not null
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

                // Find the doctor by ID
                var existinglogin = _context.LoginRegistrations.Find(id);
                if (existinglogin == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "login not found"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                // Remove the doctor
                _context.LoginRegistrations.Remove(existinglogin);

                // Save changes to the database
                _context.SaveChanges();

                return new JsonResult(new
                {
                    success = true,
                    message = "login deleted successfully"
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
                    message = ex.Message
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ActionResult<IEnumerable<LoginRegistration>>> GetAlllogin()
        {
            try
            {
                if (_context != null)
                {
                    // LINQ query to join LoginRegistration with Staff and Role
                    var query = await (from login in _context.LoginRegistrations
                                       join staff in _context.Staff on login.StaffId equals staff.StaffId
                                       join role in _context.Roles on login.RoleId equals role.RoleId
                                       select new LoginRegistration
                                       {
                                           RegistrationId = login.RegistrationId,
                                           Username = login.Username,
                                           StaffName = staff.StaffName,
                                           RoleName = role.RoleName,
                                           RisActive = login.RisActive,
                                           RegisteredDate = login.RegisteredDate
                                       }).ToListAsync();

                    return query;
                }

                return new List<LoginRegistration>();
            }
            catch (Exception ex)
            {
                // Log error if needed
                return null;
            }
        }


        public async Task<ActionResult<LoginRegistration>> Getloginbycode(int id)
        {

            try
            {
                if (_context != null)
                {
                    // Find the doctor by ID
                    var login = await _context.LoginRegistrations
                        .Include(st => st.Staff)
                        .Include(st => st.Role)
                        .FirstOrDefaultAsync(e => e.RegistrationId == id);
                    return login;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<int>> insertlogin(LoginRegistration login)
        {
            try
            {
                // Validate input
                if (login == null)
                {
                    throw new ArgumentNullException(nameof(login), "Login data is null");
                }

                // Ensure context is initialized
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }

                // Check if the username matches a StaffName from the StaffTable
                var staff = await _context.Staff.FirstOrDefaultAsync(s => s.StaffName == login.Username);
                if (staff == null)
                {
                    throw new Exception("Username does not match any staff name in the staff table.");
                }

                // Set the password to be StaffId + "KATT"
                login.Password = staff.StaffId + "KATT";

                // Add the login record to the context
                await _context.LoginRegistrations.AddAsync(login);

                // Save changes to the database
                var changesRecord = await _context.SaveChangesAsync();
                if (changesRecord > 0)
                {
                    return login.RegistrationId;
                }
                else
                {
                    throw new Exception("Failed to save login record to the database.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional, depending on your logging setup)
                return null;
            }
        }


        public async Task<ActionResult<LoginRegistration>> updatelogin(int id, LoginRegistration login)
        {
            try
            {
                // Check if doctor object is not null
                if (login == null)
                {
                    throw new ArgumentNullException(nameof(login), "Doctor data is null");
                }

                // Ensure context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }

                // Find the doctor by ID
                var existingDoctor = await _context.LoginRegistrations.FindAsync(id);
                if (existingDoctor == null)
                {
                    return null;
                }

                // Map values with fields - update
                existingDoctor.Username = login.Username;
                existingDoctor.Password = login.Password;
                existingDoctor.RisActive = login.RisActive;

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Retrieve the updated doctor with related entities
                var updatedDoctor = await _context.LoginRegistrations
                    .Include(e => e.Role)
                    .Include(e => e.Staff)
                    .FirstOrDefaultAsync(e => e.RegistrationId == login.RegistrationId);

                return updatedDoctor;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<IEnumerable<Staff>>> GetTblDepartments()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Staff.ToListAsync();
                }

                //return an empty list if context is null
                return new List<Staff>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ActionResult<IEnumerable<Role>>> GetTblDepartmentss()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Roles.ToListAsync();
                }

                //return an empty list if context is null
                return new List<Role>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        Task<ActionResult<IEnumerable<LoginRegistration>>> IRegistrationRepository.GetTblDepartments()
        {
            throw new NotImplementedException();
        }
    }
}
