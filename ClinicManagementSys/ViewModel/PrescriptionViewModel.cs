namespace ClinicManagementSys.ViewModel
{
    public class PrescriptionViewModel
    {
        public int PrescriptionId { get; set; }
        public int? AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public int? MedicineId { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public string StaffName { get; set; }
        public int NumberofDays { get; set; }
        public string PatientName { get; set; }

        public decimal? Cost { get; set; }

    }
}
