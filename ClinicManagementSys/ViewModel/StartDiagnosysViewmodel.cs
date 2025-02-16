using ClinicManagementSys.Model;

namespace ClinicManagementSys.ViewModel
{
    public class StartDiagnosysViewmodel
    {

        // public int PatientId { get; set; }

        public string PatientName { get; set; } = null!;

        public DateTime? Dob { get; set; }

        public string? Gender { get; set; }

        public string? BloodGroup { get; set; }

        public string? PatientPhone { get; set; }

        public string? PatientAddress { get; set; }

        public DateTime? RegistrationDate { get; set; }
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int TokenNumber { get; set; }


        public string? SpecializationName { get; set; }

    }
}