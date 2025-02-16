namespace ClinicManagementSys.ViewModel
{
    public class LabtestBillViewModel
    {
        public int BillId { get; set; }
        public int LtreportId { get; set; }
        public string PatientName { get; set; }
        public string PatientPhone { get; set; }
        public string TestName { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}