namespace ClinicManagementSys.ViewModel
{
    public class AppPatStaLabViewModel
    {

        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int RegistrationId { get; set; }
        public string BloodGroup { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public int LabTestId { get; set; }
        public string PatientName { get; set; }
        public DateTime? DOB { get; set; }
        public string Gender { get; set; }
        public string PatientPhone { get; set; }
        public string TestName { get; set; }
        public int TpId { get; set; }
        public decimal? HighRange { get; set; }
        public decimal? LowRange { get; set; }
        public int ActualResult { get; set; }
        public string Remarks { get; set; }
        public string? SampleItem { get; set; }
        public int TokenNumber { get; set; }
    }
}