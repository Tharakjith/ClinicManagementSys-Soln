namespace ClinicManagementSys.ViewModel
{
    public class AppointmentBookingVm
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int SpecializationId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }

        // The token for the appointment (generated sequentially)
        public int TokenNumber { get; set; }

        // Additional fields for managing appointment status and booking
        public string DoctorName { get; set; } 
        public string SpecializationName { get; set; }  
        public bool IsAvailable { get; set; }  
        public string AppointmentStatus { get; set; } 
    }

}

