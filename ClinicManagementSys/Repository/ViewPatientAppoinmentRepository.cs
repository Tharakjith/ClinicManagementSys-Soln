using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static ClinicManagementSys.Repository.ViewPatientAppoinmentRepository;

namespace ClinicManagementSys.Repository
{
    public class ViewPatientAppoinmentRepository : IViewPatientAppoinmentRepository
    {

        private readonly ClinicManagementSysContext _context;

        public ViewPatientAppoinmentRepository(ClinicManagementSysContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StartDiagnosysViewmodel>> GetTodaysAppointmentsAsync(int doctorId)
        {
            var today = DateTime.Today;

            var appointments = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Specialization)
                .Where(a => a.DoctorId == doctorId && a.AppointmentDate.Date == today)
                .OrderBy(a => a.TokenNumber)
                .Select(a => new StartDiagnosysViewmodel
                {
                    AppointmentId = a.AppointmentId,
                    TokenNumber = a.TokenNumber,
                    AppointmentDate = a.AppointmentDate,
                    PatientName = a.Patient.PatientName,
                    Gender = a.Patient.Gender,
                    BloodGroup = a.Patient.BloodGroup,
                    PatientPhone = a.Patient.PatientPhone,
                    SpecializationName = a.Specialization.SpecializationName
                })
                .ToListAsync();

            return appointments;
        }
    } 


    
}
