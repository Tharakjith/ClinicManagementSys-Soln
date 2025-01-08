namespace ClinicManagementSys.ViewModel
{
    public class MedicineDistributionViewModel
    {
        public int DistributionId { get; set; }
        public int? PrescriptionId { get; set; }
        public int? MedicineId { get; set; }
        public int QuantityDistributed { get; set; }
        public int? MedStatusId { get; set; }
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public int? NumberofDays { get; set; }
        public int StockInHand { get; set; }
        public string MedStatusName { get; set; }
    }
}
