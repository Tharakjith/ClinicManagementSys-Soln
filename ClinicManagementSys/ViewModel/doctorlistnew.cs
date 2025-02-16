namespace ClinicManagementSys.ViewModel
{
    namespace ClinicManagementSys.ViewModel
    {
        public class doctorlistnew
        {
            public int DoctorId { get; set; } // For listing purposes
            public string StaffName { get; set; }
            public int StaffId { get; set; } // For inserting the doctor
            public string? SpecializationName { get; set; } // For listing specialization
            public int? SpecializationId { get; set; } // For inserting specialization
            public decimal ConsultationFee { get; set; }
            public bool? DoctorIsActive { get; set; }
            public int RegistrationId { get; set; } // Nullable bool
        }
    }

}