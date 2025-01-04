namespace ClinicManagementSys.ViewModel
{
    public class PrescriptionBillViewModel
    {
        public int AppointmentId { get; set; }
        public int? MedicineId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string MedicineName { get; set; } 
        public DateTime BillDateTime { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public int NumberofDays { get; set; }
    }
}
