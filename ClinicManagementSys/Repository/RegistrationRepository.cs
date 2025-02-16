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
                    return await _context.LoginRegistrations.Include(s => s.Role).Include(s => s.Staff).ToListAsync();
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
        public async Task<ActionResult<IEnumerable<Role>>> Getroles()
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

        public async Task<LoginRegistration> AddLoginRegistration(LoginRegistration loginRegistration)
        {
            if (loginRegistration == null)
            {
                throw new ArgumentNullException(nameof(loginRegistration), "LoginRegistration data is null");
            }

            // Validate StaffId
            if (!await _context.Staff.AnyAsync(s => s.StaffId == loginRegistration.StaffId))
            {
                throw new InvalidOperationException("Invalid StaffId");
            }

            // Validate RoleId
            if (!await _context.Roles.AnyAsync(r => r.RoleId == loginRegistration.RoleId))
            {
                throw new InvalidOperationException("Invalid RoleId");
            }

            // Add LoginRegistration to the database
            await _context.LoginRegistrations.AddAsync(loginRegistration);
            await _context.SaveChangesAsync();

            // Return the inserted record with related entities
            var insertedLoginRegistration = await _context.LoginRegistrations
                .Include(lr => lr.Role)
                .Include(lr => lr.Staff)
                .FirstOrDefaultAsync(lr => lr.RegistrationId == loginRegistration.RegistrationId);

            return insertedLoginRegistration;
        }


    }
}