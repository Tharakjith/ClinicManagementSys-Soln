namespace ClinicManagementSys.ViewModel
{
    public class StaffDepViewModel
    {
        public string StaffName { get; set; }
        public int? StaffId { get; set; }
        public DateTime? Dob { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; } // Optional
        public string Email { get; set; }
        public string Gender { get; set; }
        public bool? StaffIsActive { get; set; }
        public string DepartmentName { get; set; }
    }
}
