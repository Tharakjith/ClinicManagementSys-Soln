namespace ClinicManagementSys.ViewModel
{
    public class LabTestReportViewModel
    {
        public int TpId { get; set; }
        public int LtreportId { get; set; }
        public int AppointmentId { get; set; }
        public int LabTestId { get; set; }
        public decimal? HighRange { get; set; }
        public decimal? LowRange { get; set; }
        public int? ActualResult { get; set; } // Already included
        public string? Remarks { get; set; }  // Already included
        public string PatientName { get; set; }
        public string BloodGroup { get; set; }
        public string Gender { get; set; }
        public string PatientPhone { get; set; }
        public string TestName { get; set; }
        public string SampleItem { get; set; }
    }
}

