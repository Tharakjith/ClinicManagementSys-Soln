using ClinicManagementSys.Model;
using ClinicManagementSys.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewPatientAppoinmentController : ControllerBase
    {
        private readonly IViewPatientAppoinmentRepository _startDiagnosysRepository;

        public ViewPatientAppoinmentController(IViewPatientAppoinmentRepository startDiagnosysRepository)
        {
            _startDiagnosysRepository = startDiagnosysRepository;
        }

        

        /// <returns>A list of today's appointments.</returns>
        [HttpGet("todaysAppointments/{doctorId}")]
        public async Task<IActionResult> GetTodaysAppointments(int doctorId)
        {
            try
            {
                // Fetch today's appointments for the doctor.
                var appointments = await _startDiagnosysRepository.GetTodaysAppointmentsAsync(doctorId);

                if (appointments == null || !appointments.Any())
                {
                    return NotFound(new { Message = "No appointments found for today." });
                }

                return Ok(appointments);
            }
            catch (Exception ex)
            {
                // Log the error (you can use a logging library like Serilog here).
                // For simplicity:
                Console.WriteLine($"Error fetching today's appointments: {ex.Message}");

                // Return a 500 Internal Server Error with details.
                return StatusCode(500, new { Message = "An error occurred while processing your request." });
            }
        }
    }
}
