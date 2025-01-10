namespace ClinicManagementSys.ViewModel
{
    public class LabTestReportViewModel
    {
        public int TpId { get; set; }
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int LabTestId { get; set; }
        public string TestName { get; set; }
        public int HighRange { get; set; }
        public int LowRange { get; set; }
        public int ActualResult { get; set; }
        public string Remarks { get; set; }


    }
}
