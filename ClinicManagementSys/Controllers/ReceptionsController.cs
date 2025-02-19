﻿using ClinicManagementSys.Model;
using ClinicManagementSys.Repository;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClinicManagementSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionsController : ControllerBase
    {

        private readonly IReceptionistRepository _repository;

        //DI - Dependency Injection
        public ReceptionsController(IReceptionistRepository repository)
        {
            _repository = repository;
        }

        #region PATIENT Registration

        #region  1 -  Get all patients from DB 
        [HttpGet]
        // [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
        {
            var patients = await _repository.GetPatients();
            if (patients == null)
            {
                return NotFound("No Patients found");
            }
            return Ok(patients);
        }
        #endregion

        #region 2 - Get an Patient based on Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetAllPatientById(int id)
        {
            var patient = await _repository.GetPatientById(id);
            if (patient == null)
            {
                return NotFound("Oops! No Patient found on this Id");
            }
            return Ok(patient);
        }
        #endregion

        #region  3 -  Get Patient by phone number
        [HttpGet("phone/{phoneNumber}")]
        public async Task<IActionResult> GetPatientByPhone(string phoneNumber)
        {
            var PatPhone = await _repository.GetPatientByPhoneNumber(phoneNumber);
            if (PatPhone == null)
            {
                return NotFound("Oops! No Patient found on this phoneNumber");
            }
            return Ok(PatPhone);
        }
        #endregion

        #region  4  - Insert an Patient -return Patient record
        [HttpPost]
        public async Task<ActionResult<Patient>> InsertTblPatientsReturnRecord(Patient patient)
        {
            if (ModelState.IsValid)
            {
                //insert a new record and return as an object named patient
                var newPatient = await _repository.PostPatientReturnRecord(patient);
                if (newPatient != null)
                {
                    return Ok(newPatient);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region    5 - Update/Edit an Patient with ID
        [HttpPut("{id}")]
        public async Task<ActionResult<Patient>> PutPatientDetail(int id, Patient patient)
        {
            if (ModelState.IsValid)
            {
                var updatePatient = await _repository.PutPatient(id, patient);
                if (updatePatient != null)
                {
                    return Ok(updatePatient);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region  6  - Delete an Patient by id
        [HttpDelete("{id}")]
        public IActionResult DeletePatientDetail(int id)
        {
            try
            {
                var result = _repository.DeletePatient(id);

                if (result == null)
                {
                    //if result indicates failure or null
                    return NotFound(new
                    {
                        success = false,
                        message = "Patient could not be deleted or not found"
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexpected error occurs" });
            }
        }
        #endregion

        #endregion

        #region APPOINTMENT BOOKING 

        #region 1 -  Get all Appointment from DB 
        [HttpGet("Appointments")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
        {
            var appointments = await _repository.GetAppointments();
            if (appointments == null)
            {
                return NotFound("No Appointments found");
            }
            return Ok(appointments);
        }
        #endregion

        #region 2 -  Get all Specializations from DB 
        [HttpGet("Specializations")]
        public async Task<ActionResult<IEnumerable<Specialization>>> GetAllSpecializations()
        {
            var specializations = await _repository.GetSpecializations();
            if (specializations == null)
            {
                return NotFound("No Specializations found");
            }
            return Ok(specializations);
        }

        #endregion

        #region 3 -  Get all doctors from DB 
        [HttpGet("Doctors")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
        {
            var doctors = await _repository.GetDoctors();
            if (doctors == null)
            {
                return NotFound("No Doctors found");
            }
            return Ok(doctors);
        }
        #endregion

        #region 4 - Get doctors by specialization
        [HttpGet("Doctors/{specializationId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetDoctorsBySpecialization(int specializationId)
        {
            try
            {
                var doctors = await _repository.GetDoctorsBySpecializationWithStaffDetails(specializationId);

                // Check if doctors is null or empty
                if (doctors == null || !doctors.Any())
                {
                    return NotFound("No doctors found for the selected specialization");
                }

                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region 5 - Get doctor's consultation fee by doctor's id
        [HttpGet("doctor/{doctorId}/consultation-fee")]
        public async Task<ActionResult<decimal>> GetConsultationFeeByDoctorId(int doctorId)
        {
            try
            {
                // Call the repository method to fetch the consultation fee
                var consultationFee = await _repository.GetConsultationFeeByDoctorId(doctorId);

                // Return the consultation fee
                return consultationFee;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region 6 - Get doctor availability by doctorId
        [HttpGet("DoctorAvailability/{doctorId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetDoctorAvailability(int doctorId)
        {
            try
            {
                var availability = await _repository.GetDoctorAvailability(doctorId);

                if (availability == null || !availability.Any())
                {
                    return NotFound("No availability found for the selected doctor.");
                }

                return Ok(availability);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region 7 - Generate Token Number for Appointment
        [HttpGet("generatetoken/{doctorId}/{appointmentDate}/{timeSlotId}")]
        public async Task<ActionResult<int>> GenerateTokenNumber(int doctorId, DateTime appointmentDate, int timeSlotId)
        {
            try
            {
                // Call the repository method to generate the token number
                var tokenNumber = await _repository.GenerateTokenNumber(doctorId, appointmentDate, timeSlotId);

                // Return the generated token number
                return Ok(new { success = true, tokenNumber });
            }
            catch (InvalidOperationException ex)
            {
                // Return an error response if the token limit is reached or another validation error occurs
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                // Return a generic error response for unexpected exceptions
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region 8 - Book an Appointment
        [HttpPost("Bookappointment")]
        public async Task<ActionResult<Appointment>> BookAppointment([FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            try
            {
                // Validate required fields
                if (appointment.PatientId <= 0 || appointment.DoctorId <= 0 ||
                    appointment.SpecializationId <= 0 || appointment.AvailabilityId <= 0)
                {
                    return BadRequest(new { success = false, message = "Required fields are missing or invalid" });
                }

                // Generate consultation fee based on doctor
                var consultationFee = await _repository.GetConsultationFeeByDoctorId(appointment.DoctorId);
                appointment.ConsultationFee = consultationFee;

                // Call the repository method to book the appointment
                var bookedAppointment = await _repository.BookAppointment(appointment);

                // Return the booked appointment details
                return Ok(new
                {
                    success = true,
                    appointment = new
                    {
                        bookedAppointment.Value.AppointmentId,
                        bookedAppointment.Value.PatientId,
                        bookedAppointment.Value.DoctorId,
                        bookedAppointment.Value.AppointmentDate,
                        bookedAppointment.Value.TokenNumber,
                        bookedAppointment.Value.ConsultationFee,
                        bookedAppointment.Value.RegistrationFee,
                        bookedAppointment.Value.AppointmentStatusId,
                        DoctorName = bookedAppointment.Value.Doctor?.Registration?.Staff?.StaffName,
                        PatientName = bookedAppointment.Value.Patient?.PatientName,
                        SpecializationName = bookedAppointment.Value.Specialization?.SpecializationName,
                        Status = bookedAppointment.Value.AppointmentStatus?.AppointmentStatus1
                    }
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while booking the appointment." });
            }
        }
        #endregion

        #region 9 - Patient Bill generate View model
        [HttpGet("bill/{patientId}")]
        public async Task<IActionResult> GetPatientBill(int patientId)
        {
            var patientBill = await _repository.GetPatientBillByIdAsync(patientId);
            if (patientBill == null)
            {
                return NotFound(new { Message = "Patient or appointment details not found." });
            }

            return Ok(patientBill);
        }
        #endregion

        #endregion
    }
}