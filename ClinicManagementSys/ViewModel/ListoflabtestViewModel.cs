namespace ClinicManagementSys.ViewModel
{
    public class ListoflabtestViewModel
    {
        public int TokenNumber { get; set; }
        public string PatientName { get; set; }
        public string BloodGroup { get; set; }
        public int AppointmentId { get; set; }

        public string Gender { get; set; }

        public DateTime? TestDate { get; set; }

        public int RegistrationId { get; set; }

        public int? StaffId { get; set; }

        public int? RoleId { get; set; }

        public string? StaffName { get; set; }
        public int DoctorId { get; set; }
        public string TestName { get; set; }
    }
}
