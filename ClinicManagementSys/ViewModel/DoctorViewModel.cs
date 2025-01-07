namespace ClinicManagementSys.ViewModel
{
    public class DoctorViewModel
    {
        public int? StaffId { get; set; }
        public string StaffName { get; set; } // StaffName used as DoctorName
        public DateTime? Dob { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string SpecializationName { get; set; } // Specialization name
        public decimal? ConsultationFee { get; set; }
        public int? RegistrationId { get; set; }
    }
}
