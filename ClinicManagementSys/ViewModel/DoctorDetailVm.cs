namespace ClinicManagementSys.ViewModel
{
    public class DoctorDetailVm
    {
        public int DoctorId { get; set; }

        public int StaffId { get; set; }

        public string? StaffName { get; set; }

        public DateTime? Dob { get; set; }

        public int? SpecializationId { get; set; }

        public decimal? ConsultationFee { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? Gender { get; set; }

        public int? DepartmentId { get; set; }

        public bool? StaffIsActive { get; set; }
    }
}
