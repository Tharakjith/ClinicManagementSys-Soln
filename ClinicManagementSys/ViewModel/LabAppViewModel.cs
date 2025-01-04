namespace ClinicManagementSys.ViewModel
{
    public class LabAppViewModel
    {
        public int LTReportId { get; set; }
        public int AppointmentId { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public int PatientId {  get; set; }
        public string PatientName { get; set; }
        public int DoctorId { get; set; }
        public int LabTestId { get; set; }
        public int HighRange { get; set; }
        public int LowRange { get; set; }
        public int ActualResult { get; set; }
        public string Remarks { get; set; }
    }
}

