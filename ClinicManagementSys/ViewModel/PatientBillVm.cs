namespace ClinicManagementSys.ViewModel
{
    public class PatientBillVm
    {
        public int PatientId { get; set; }

        public string PatientName { get; set; } = null!;

        public string? PatientPhone { get; set; }

        public string? PatientAddress { get; set; }

        public int AppointmentId { get; set; }

        public int DoctorId { get; set; }

        public string? StaffName { get; set; }

        public int TokenNumber { get; set; }

        public DateTime AppointmentDate { get; set; }

        public decimal ConsultationFee { get; set; }

        public decimal RegistrationFee { get; set; }
    }
}