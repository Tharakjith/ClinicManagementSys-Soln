using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using ClinicManagementSys.ViewModel.ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSys.Repository
{
    public class DoctorsRepository : IDoctorsRepository
    {
        private readonly ClinicManagementSysContext _context;

        public DoctorsRepository(ClinicManagementSysContext context)
        {
            _context = context;
        }

        public JsonResult Deletedoctor(int id)
        {
            try
            {
                // Check if the ID is valid
                if (id <= 0)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid doctor ID"
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
                var existingdoctor = _context.Doctors.Find(id);
                if (existingdoctor == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Doctor not found"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                // Remove the doctor
                _context.Doctors.Remove(existingdoctor);

                // Save changes to the database
                _context.SaveChanges();

                return new JsonResult(new
                {
                    success = true,
                    message = "Doctor deleted successfully"
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

        public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
        {
            try
            {
                if (_context != null)
                {
                    var doctors = await _context.Doctors
                        .Include(s => s.Specialization)
                        .Include(s => s.Registration)
                           .ThenInclude(r => r.Staff)
                        .ToListAsync();
                    return doctors;
                }

                return new List<Doctor>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<Doctor>> GetDoctorbycode(int id)
        {
            try
            {
                if (_context != null)
                {
                    // Find the doctor by ID
                    var doctor = await _context.Doctors
                        .Include(st => st.Specialization)
                        .Include(st => st.Registration)
                        .FirstOrDefaultAsync(e => e.DoctorId == id);
                    return doctor;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<int>> insertDoctor(Doctor doctor)
        {
            try
            {
                // Check if doctor object is not null
                if (doctor == null)
                {
                    throw new ArgumentNullException(nameof(doctor), "Doctor data is null");
                }

                // Ensure context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }

                // Add the doctor record to the context
                await _context.Doctors.AddAsync(doctor);

                // Save changes to the database
                var changesRecord = await _context.SaveChangesAsync();
                if (changesRecord > 0)
                {
                    return doctor.DoctorId;
                }
                else
                {
                    throw new Exception("Failed to save doctor record to the database");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<Doctor>> updateDoctor(int id, Doctor doctor)
        {
            try
            {
                // Check if doctor object is not null
                if (doctor == null)
                {
                    throw new ArgumentNullException(nameof(doctor), "Doctor data is null");
                }

                // Ensure context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }

                // Find the doctor by ID
                var existingDoctor = await _context.Doctors.FindAsync(id);
                if (existingDoctor == null)
                {
                    return null;
                }

                // Map values with fields - update
                existingDoctor.ConsultationFee = doctor.ConsultationFee;
                existingDoctor.DoctorIsActive = doctor.DoctorIsActive;

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Retrieve the updated doctor with related entities
                var updatedDoctor = await _context.Doctors
                    .Include(e => e.Specialization)
                    .Include(e => e.Registration)
                    .FirstOrDefaultAsync(e => e.DoctorId == doctor.DoctorId);

                return updatedDoctor;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DoctorViewModel> GetDoctorDetailsByPhoneAsync(string phoneNumber)
        {
            try
            {
                // Build the query to fetch doctor details by phone number, join with Staff and Specialization
                var query = _context.Doctors
                    .Include(d => d.Specialization) // Assuming the Doctor has a navigation property for Specialization
                    .Join(_context.Staff, // Join Doctor table with Staff table on StaffId
                        doctor => doctor.RegistrationId, // Doctor's RegistrationId is linked to StaffId
                        staff => staff.StaffId,
                        (doctor, staff) => new { Doctor = doctor, Staffs = staff }) // The Staff table is aliased as Staffs
                    .Where(ds => !string.IsNullOrEmpty(phoneNumber) && ds.Staffs.PhoneNumber == phoneNumber) // Corrected to use ds.Staffs
                    .Select(ds => new DoctorViewModel
                    {
                        StaffId = ds.Staffs.StaffId,
                        StaffName = ds.Staffs.StaffName,
                        Dob = ds.Staffs.Dob,
                        Address = ds.Staffs.Address,
                        PhoneNumber = ds.Staffs.PhoneNumber,
                        SpecializationName = ds.Doctor.Specialization.SpecializationName, // Assuming Specialization has a "SpecializationName" field
                        ConsultationFee = ds.Doctor.ConsultationFee,
                        RegistrationId = ds.Doctor.RegistrationId
                    });

                // Return the first matching doctor or null if none are found
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (optional) and rethrow for troubleshooting
                throw new Exception("Error retrieving doctor details by phone number.", ex);
            }
        }




        public async Task<bool> AddDoctorAvailabilityAsync(DoctorAvailabilityViewModel model)
        {
            try
            {
                // Find the DoctorId from Staff Name
                var doctorId = await (from staff in _context.Staff
                                      join login in _context.LoginRegistrations on staff.StaffId equals login.StaffId
                                      join doctor in _context.Doctors on login.RegistrationId equals doctor.RegistrationId
                                      where staff.StaffName == model.StaffName
                                      select doctor.DoctorId).FirstOrDefaultAsync();

                if (doctorId == 0)
                    throw new Exception("Doctor not found");

                // Find the WeekdayId
                var weekdayId = await _context.Weekdays
                    .Where(w => w.WeekdaysName == model.WeekdaysName)
                    .Select(w => w.WeekdaysId)
                    .FirstOrDefaultAsync();

                if (weekdayId == 0)
                    throw new Exception("Weekday not found");

                // Find or Create the TimeSlot
                var timeSlot = await _context.Timeslots
                    .FirstOrDefaultAsync(t => t.StartTime == model.StartTime && t.EndTime == model.EndTime && t.WeekdaysId == weekdayId);

                if (timeSlot == null)
                {
                    timeSlot = new Timeslot
                    {
                        StartTime = model.StartTime,
                        EndTime = model.EndTime,
                        WeekdaysId = weekdayId
                    };
                    _context.Timeslots.Add(timeSlot);
                    await _context.SaveChangesAsync();
                }

                // Add to Availability
                var availability = new Availability
                {
                    DoctorId = doctorId,
                    TimeSlotId = timeSlot.TimeSlotId,
                    Session = model.Session
                };

                _context.Availabilities.Add(availability);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log error here
                return false;
            }

        }
        public async Task<ActionResult<Doctor>> postTblEmployeesReturnRecord(Doctor employee)
        {
            try
            {//check idf employee object is not null
                if (employee == null)
                {
                    throw new ArgumentNullException(nameof(employee), "Employee data is null");

                }
                // ensure context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }
                //add the employee record to the dbcontext
                await _context.Doctors.AddAsync(employee);

                //save changes to the database
                await _context.SaveChangesAsync();
                //retrive the employee with the related departement
                var employeewithDepartment = await _context.Doctors.Include(e => e.Registration).Include(e => e.Specialization)
                    .FirstOrDefaultAsync(e => e.DoctorId == employee.DoctorId);
                return employeewithDepartment;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<IEnumerable<Specialization>>> GetTblDepartments()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Specializations.ToListAsync();
                }

                //return an empty list if context is null
                return new List<Specialization>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ActionResult<IEnumerable<LoginRegistration>>> GetTblUsers()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.LoginRegistrations.ToListAsync();
                }

                //return an empty list if context is null
                return new List<LoginRegistration>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ActionResult<IEnumerable<Staff>>> GetTblstaffs()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Staff.ToListAsync();
                }


                return new List<Staff>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        /////////////////////////////////////////////////////////////////////////////////////
        ///





        public bool RegisterDoctor(doctorlistnew model)
        {
            // Fetch the Staff record based on StaffName
            var staff = _context.Staff.FirstOrDefault(s => s.StaffName == model.StaffName);
            if (staff == null)
            {
                return false;
            }


            var registration = _context.LoginRegistrations
                .FirstOrDefault(r => r.StaffId == staff.StaffId && r.RoleId == 2 && r.RisActive);
            if (registration == null)
            {
                return false;
            }


            var existingDoctor = _context.Doctors
                .FirstOrDefault(d => d.RegistrationId == registration.RegistrationId);
            if (existingDoctor != null)
            {
                return false;
            }


            var doctor = new Doctor
            {
                RegistrationId = registration.RegistrationId,
                SpecializationId = model.SpecializationId,
                ConsultationFee = model.ConsultationFee,
                DoctorIsActive = model.DoctorIsActive
            };

            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            return true;
        }

        public async Task<ActionResult<IEnumerable<Weekday>>> listallweekdays()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Weekdays.ToListAsync();
                }

                //return an empty list if context is null
                return new List<Weekday>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Timeslot>> listalltimeslotsrtments()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Timeslots
                        .Include(t => t.Weekdays)  // Note: Weekdays instead of Weekday
                        .Select(t => new Timeslot
                        {
                            TimeSlotId = t.TimeSlotId,
                            StartTime = t.StartTime,
                            EndTime = t.EndTime,
                            WeekdaysId = t.WeekdaysId,
                            Weekdays = t.Weekdays != null ? new Weekday  // Note: Weekday instead of Weekdays for the class
                            {
                                WeekdaysId = t.Weekdays.WeekdaysId,
                                WeekdaysName = t.Weekdays.WeekdaysName
                            } : null,
                            Availabilities = new List<Availability>()  // Initialize empty availability list
                        })
                        .ToListAsync();
                }
                return new List<Timeslot>();
            }
            catch (Exception ex)
            {
                // Log the exception if you have logging configured
                Console.WriteLine($"Error in listalltimeslotsrtments: {ex.Message}");
                return null;
            }
        }

        //AVAILABILITY
        //public async Task<bool> InsertAvailabilityAsync(Availability availability)
        //{
        //    try
        //    {
        //        _context.Availabilities.Add(availability);
        //        var result = await _context.SaveChangesAsync();
        //        return result > 0;
        //    }
        //    catch (DbUpdateException)
        //    {
        //        throw new Exception("A database error occurred while saving the availability.");
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("An unexpected error occurred while inserting availability.");
        //    }
        //}

        //public async Task<Doctor?> GetDoctorByIdAsync(int doctorId)
        //{
        //    try
        //    {
        //        return await _context.Doctors.FindAsync(doctorId);
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("An error occurred while retrieving doctor details.");
        //    }
        //}

        //public async Task<Timeslot?> GetTimeslotByIdAsync(int timeslotId)
        //{
        //    try
        //    {
        //        return await _context.Timeslots.FindAsync(timeslotId);
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("An error occurred while retrieving timeslot details.");
        //    }
        //}


        public async Task<object> AddAvailability(Availability availability)
        {
            _context.Availabilities.Add(availability);
            await _context.SaveChangesAsync();

            // Fetch the complete details including TimeSlot and Weekday information
            var timeSlot = await GetTimeSlotDetails(availability.TimeSlotId.Value);

            return new
            {
                AvailabilityId = availability.AvailabilityId,
                DoctorId = availability.DoctorId,
                TimeSlot = new
                {
                    StartTime = timeSlot.StartTime,
                    EndTime = timeSlot.EndTime,
                    WeekdayName = timeSlot.Weekdays.WeekdaysName
                },
                Session = availability.Session
            };
        }

        public async Task<Timeslot> GetTimeSlotDetails(int timeSlotId)
        {
            return await _context.Timeslots
                .Include(t => t.Weekdays)
                .FirstOrDefaultAsync(t => t.TimeSlotId == timeSlotId);
        }

        public async Task<IEnumerable<Timeslot>> GetAllTimeslots()
        {
            return await _context.Timeslots
                .Include(t => t.Weekdays)
                .ToListAsync();
        }
    }
}