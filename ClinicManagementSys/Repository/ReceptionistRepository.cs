using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClinicManagementSys.Repository
{
    public class ReceptionistRepository : IReceptionistRepository
    {
        private readonly ClinicManagementSysContext _context;

        public ReceptionistRepository(ClinicManagementSysContext context)
        {
            _context = context;
        }

        #region  PATIENT Registration

        #region 1 -  Get all patients from DB 
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Patients.ToListAsync();
                }
                //Returns an empty list if context is null
                return new List<Patient>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error fetching Patient list: {ex.Message}");
            }
        }
        #endregion

        #region   2 - Get an Patient based on Id
        public async Task<ActionResult<Patient>> GetPatientById(int id)
        {
            try
            {
                if (_context != null)
                {
                    var patient = await _context.Patients.FirstOrDefaultAsync(p => p.PatientId == id);
                    return patient;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error fetching Patient by Id: {ex.Message}");
            }
        }
        #endregion

        #region  3 - Get Patient by phone number
        public async Task<ActionResult<Patient>> GetPatientByPhoneNumber(string phoneNumber)
        {
            try
            {
                if (_context != null)
                {
                    var patientNum = await _context.Patients.FirstOrDefaultAsync(p => p.PatientPhone == phoneNumber);
                    return patientNum;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error fetching patient by phone number: {ex.Message}");
            }
        }

        #endregion

        #region  4  - Insert an Patient -return Patient record
        public async Task<ActionResult<Patient>> PostPatientReturnRecord(Patient patient)
        {
            try
            {
                //check if patient object is not null
                if (patient == null)
                {
                    throw new ArgumentException(nameof(patient), "Patient data is null");
                }
                //Ensure the context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                //Add the Patient record to the DBContext
                await _context.Patients.AddAsync(patient);

                //save changes to the database
                await _context.SaveChangesAsync();

                //Retrieve the Patient detail
                var newpatient = await _context.Patients.FirstOrDefaultAsync(p => p.PatientId == patient.PatientId);

                //Return the added Patient with the record added
                return newpatient;
            }

            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error in registering patient: {ex.Message}");
            }
        }
        #endregion

        #region  5  - Update/Edit an Patient with ID
        public async Task<ActionResult<Patient>> PutPatient(int id, Patient patient)
        {
            try
            {
                if (patient == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                //Find the patient by id
                var existingPatient = await _context.Patients.FindAsync(id);
                if (existingPatient == null)
                {
                    return null;
                }

                //Map values wit fields
                existingPatient.PatientPhone = patient.PatientPhone;
                existingPatient.PatientAddress = patient.PatientAddress;

                //save changes to the database
                await _context.SaveChangesAsync();

                //Retreive the patient 
                var updatePatient = await _context.Patients
                    .FirstOrDefaultAsync(existingPatient => existingPatient.PatientId == patient.PatientId);

                //Return the added Patient record
                return updatePatient;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 6  - Delete an Patient by id
        public JsonResult DeletePatient(int id)
        {
            try
            {
                if (id <= null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid Patient Id"
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
                var existingPatient = _context.Patients.Find(id);

                if (existingPatient == null)
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
                _context.Patients.Remove(existingPatient);

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
        #endregion

        #endregion

        #region APPOINTMENT BOOKING

        #region 1 -  Get all Appointment from DB 
        //public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        //{
        //    try
        //    {
        //        if (_context != null)
        //        {
        //            return await _context.Appointments.Include(e => e.Patient).Include(e => e.Specialization).Include(e => e.Doctor)
        //           .Include(e => e.DailyAvailability).Include(e => e.AppointmentStatus).ToListAsync();
        //        }
        //        //Returns an empty list if context is null
        //        return new List<Appointment>();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new InvalidOperationException($"Error fetching from Specialization's doctors: {ex.Message}");
        //    }
        //}

        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Appointments
                        .Include(a => a.Patient)
                        .Include(a => a.Specialization)
                        .Include(a => a.Doctor)
                        .Include(a => a.Availability)
                        .Include(a => a.AppointmentStatus)
                        .ToListAsync();
                }
                return new List<Appointment>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error fetching Appointments: {ex.Message}");
            }
        }

        #endregion

        #region 2 -  Get all Specialization from DB 
        //public async Task<ActionResult<IEnumerable<Specialization>>> GetSpecializations()
        //{
        //    try
        //    {
        //        if (_context != null)
        //        {
        //            return await _context.Specializations.ToListAsync();
        //        }
        //        Returns an empty list if context is null
        //        return new List<Specialization>();
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public async Task<ActionResult<IEnumerable<Specialization>>> GetSpecializations()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Specializations.ToListAsync();
                }
                return new List<Specialization>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error fetching Specializations: {ex.Message}");
            }
        }

        #endregion

        #region 3 -  Get all doctors from DB 
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Doctors.Include(e => e.Specialization).Include(e => e.Registration)
                        .ToListAsync();
                }
                //Returns an empty list if context is null
                return new List<Doctor>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error fetching doctors: {ex.Message}");
            }
        }
        #endregion

        #region  Insert - Appointment Booking
        //public async Task<ActionResult<Appointment>> PostBookAppointment(Appointment appointment)
        //{
        //    try
        //    {
        //        if (appointment == null || _context == null)
        //            throw new ArgumentNullException(nameof(appointment));

        //        // Check for DailyAvailability
        //        var dailyAvailability = await _context.DailyAvailabilities
        //            .FirstOrDefaultAsync(d => d.Availability.DoctorId == appointment.DoctorId && d.AvailableDate.Date == appointment.AppointmentDate);

        //        if (dailyAvailability == null || !(dailyAvailability.IsAvailable ?? false))
        //            throw new InvalidOperationException("Doctor is not available on the selected date.");

        //        // Check the number of existing appointments for the doctor on the given date
        //        var existingAppointments = await _context.Appointments
        //            .Where(a => a.DoctorId == appointment.DoctorId && a.AppointmentDate.Date == appointment.AppointmentDate.Date)
        //            .ToListAsync();

        //        if (existingAppointments.Count >= 20)
        //            throw new InvalidOperationException("Maximum appointment limit reached for the selected doctor on the chosen date.");

        //        // Generate the next token
        //        int nextToken = existingAppointments.Count + 1;

        //        // Create and save the appointment
        //        var newAppointment = new Appointment
        //        {
        //            PatientId = appointment.PatientId,
        //            SpecializationId = appointment.SpecializationId,
        //            DoctorId = appointment.DoctorId,
        //            AppointmentDate = appointment.AppointmentDate,
        //            TokenNumber = nextToken,
        //            AppointmentStatusId = 1 // Assuming 1 is for 'Booked'
        //        };

        //        await _context.Appointments.AddAsync(newAppointment);
        //        await _context.SaveChangesAsync();

        //        // Update the ViewModel with generated appointment details
        //        appointment.TokenNumber = nextToken;
        //        appointment.AppointmentId = newAppointment.AppointmentId;

        //        return appointment;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new InvalidOperationException($"Error booking appointment: {ex.Message}");
        //    }
        //}
        #endregion

        #region  4 - Get all Doctors based on Specialization
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorsBySpecialization(int specializationId)
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Doctors
                        .Where(d => d.SpecializationId == specializationId)
                        .Include(d => d.Specialization)
                        .ToListAsync();
                }
                return new List<Doctor>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error fetching from Specialization's doctors: {ex.Message}");
            }
        }
        #endregion

        #region   Get Daily Availability of a Doctor
        //public async Task<ActionResult<IEnumerable<DailyAvailability>>> GetDoctorDailyAvailability(int doctorId, DateTime date)
        //{
        //    try
        //    {
        //        if (_context != null)
        //        {
        //            //// Fetching DailyAvailability by DoctorId and AvailableDate
        //            //var dailyAvailabilities = await _context.DailyAvailabilities
        //            //    .Where(d => d.Availability.DoctorId == doctorId && d.AvailableDate.Date == date.Date)
        //            //    .Include(d => d.Availability.Doctor)
        //            //    .ToListAsync();

        //            //return dailyAvailabilities;
        //        }
        //        return new List<DailyAvailability>();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new InvalidOperationException($"Error fetching doctor's daily availability: {ex.Message}");
        //    }
        //}
        #endregion

        #region 5 - Get Doctor's Daily Availability by Doctor ID and Date
        public async Task<Weekday> GetWeekdayByName(string dayName)
        {
            return await _context.Weekdays.FirstOrDefaultAsync(w => w.WeekdaysName.Equals(dayName, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<IEnumerable<Availability>> GetAvailabilityByDoctorIdAndWeekday(int doctorId, int weekdayId)
        {
            return await _context.Availabilities
                .Where(a => a.DoctorId == doctorId && a.TimeSlot.WeekdaysId == weekdayId)
                .Include(a => a.TimeSlot)
                .ToListAsync();
        }
        public async Task<ActionResult<IEnumerable<Availability>>> GetDoctorAvailabilityByDoctorIdAndDate(int doctorId, DateTime date)
        {
            try
            {
                if (_context != null)
                {
                    // Step 1: Determine the name of the day for the given date
                    string dayName = date.DayOfWeek.ToString();

                    // Step 2: Fetch the weekday ID from the weekdays table
                    var weekday = await _context.Weekdays
                        .FirstOrDefaultAsync(w => w.WeekdaysName.Equals(dayName, StringComparison.OrdinalIgnoreCase));

                    if (weekday == null)
                    {
                        return new List<Availability>(); // Return empty if the weekday is not found
                    }

                    // Step 3: Find the doctor's availability for that weekday
                    var availability = await _context.Availabilities
                        .Where(a => a.DoctorId == doctorId && a.TimeSlot.WeekdaysId == weekday.WeekdaysId)
                        .Include(a => a.TimeSlot) // Include timeslot details
                        .ToListAsync();

                    return availability; // Return the availability details
                }

                return new List<Availability>(); // Return empty if the context is null
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error fetching doctor's availability: {ex.Message}");
            }
        }
        #endregion

        #region 6 - Get Consultation Fee by Doctor ID
        public async Task<decimal> GetConsultationFeeByDoctorId(int doctorId)
        {
            try
            {
                if (_context != null)
                {
                    // Fetch the consultation fee of the specified doctor
                    var consultationFee = await _context.Doctors
                        .Where(d => d.DoctorId == doctorId)
                        .Select(d => d.ConsultationFee)
                        .FirstOrDefaultAsync();

                    // Check if the doctor was found
                    if (consultationFee != 0)
                    {
                        //return consultationFee;

                    }

                    throw new InvalidOperationException("Doctor not found");
                }

                throw new InvalidOperationException("Database context is not initialized");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it (e.g., using a logger)
                Console.WriteLine($"Error fetching consultation fee: {ex.Message}");

                // Throw a generic exception with a relevant message
                throw new InvalidOperationException("An error occurred while fetching the consultation fee");
            }
        }
        #endregion

        #region 7 - Auto-generate Token Number for Appointments
        public async Task<int> GenerateTokenNumber(int doctorId, DateTime appointmentDate)
        {
            try
            {
                if (_context != null)
                {
                    var existingAppointments = await _context.Appointments
                        .Where(a => a.DoctorId == doctorId && a.AppointmentDate.Date == appointmentDate.Date)
                        .ToListAsync();

                    return existingAppointments.Count + 1; // Tokens will be sequential
                }
                throw new InvalidOperationException("Database context is not initialized");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error generating token number: {ex.Message}");
            }
        }
        #endregion

        #region 8 - Insert Appointment and return success
        public async Task<ActionResult<Appointment>> BookAppointment(Appointment appointment)
        {
            try
            {
                if (appointment == null || _context == null)
                    throw new ArgumentNullException(nameof(appointment));

                // Check if the doctor is available on the given date
                var dailyAvailability = await _context.Availabilities
                    .FirstOrDefaultAsync(d => d.DoctorId == appointment.DoctorId);

                // Generate Token Number
                var tokenNumber = await GenerateTokenNumber(appointment.DoctorId, appointment.AppointmentDate);

                // Set the token number and appointment status
                appointment.TokenNumber = tokenNumber;
                appointment.AppointmentStatusId = 1; // Assuming 1 is for 'Booked'

                // Save the appointment
                await _context.Appointments.AddAsync(appointment);
                await _context.SaveChangesAsync();

                return appointment; // Return the booked appointment
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error booking appointment: {ex.Message}");
            }
        }
        #endregion

        #endregion

    }
}

