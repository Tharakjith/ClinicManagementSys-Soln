using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace ClinicManagementSys.Repository
{
    public class LabRepository : ILabRepository
    {
        private readonly ClinicManagementSysContext _context;

        public LabRepository(ClinicManagementSysContext context)
        {
            _context = context;
        }

        public JsonResult Deletelabtests(int id)
        {
            try
            {
                // Check if the ID is valid
                if (id <= 0)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid Labtests ID"
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
                var existinglabtest = _context.Labtests.Find(id);
                if (existinglabtest == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Labtests not found"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                // Remove the Labtests
                _context.Labtests.Remove(existinglabtest);

                // Save changes to the database
                _context.SaveChanges();

                return new JsonResult(new
                {
                    success = true,
                    message = "Labtest deleted successfully"
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

        public async Task<ActionResult<IEnumerable<Labtest>>> GetAllLabtest()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Labtests

                        .ToListAsync();
                }

                return new List<Labtest>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<Labtest>> GetLabtestbycode(int id)
        {
            try
            {
                if (_context != null)
                {
                    // Find the doctor by ID
                    var la = await _context.Labtests

                        .FirstOrDefaultAsync(e => e.LabTestId == id);
                    return la;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<int>> insertLabtest(Labtest labtests)
        {
            try
            {
                // Check if doctor object is not null
                if (labtests == null)
                {
                    throw new ArgumentNullException(nameof(labtests), "labtests data is null");
                }

                // Ensure context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }

                // Add the doctor record to the context
                await _context.Labtests.AddAsync(labtests);

                // Save changes to the database
                var changesRecord = await _context.SaveChangesAsync();
                if (changesRecord > 0)
                {
                    return labtests.LabTestId;
                }
                else
                {
                    throw new Exception("Failed to save labtests record to the database");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<Labtest>> updateLabtestf(int id, Labtest labtests)
        {
            try
            {
                // Check if labtests object is not null
                if (labtests == null)
                {
                    throw new ArgumentNullException(nameof(labtests), "labtests data is null");
                }

                // Ensure context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }

                // Find the labtests by ID
                var existinglabtest = await _context.Labtests.FindAsync(id);
                if (existinglabtest == null)
                {
                    return null;
                }

                // Map values with fields - update
                existinglabtest.Price = labtests.Price;
                existinglabtest.IsActive = labtests.IsActive;

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Retrieve the updated doctor with related entities
                var updatedlabtests = await _context.Labtests

                    .FirstOrDefaultAsync(e => e.LabTestId == labtests.LabTestId);

                return updatedlabtests;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ActionResult<Labtest>> postTblEmployeesReturnRecord(Labtest employee)
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

               

                // Add employee to DbContext
                await _context.Labtests.AddAsync(employee);
                await _context.SaveChangesAsync();

                // Retrieve the employee with the related department
                var employeewithDepartment = await _context.Labtests
                    .FirstOrDefaultAsync(e => e.LabTestId == employee.LabTestId);

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
