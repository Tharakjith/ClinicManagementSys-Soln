namespace ClinicManagementSys.ViewModel
{
    public class DoctorAvailabilityViewModel
    {
        public int StaffId { get; set; }

        public string? StaffName { get; set; }

        public string? WeekdaysName { get; set; }
        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }
        public int AvailabilityId { get; set; }

        public int? DoctorId { get; set; }

        public int? TimeSlotId { get; set; }

        public string? Session { get; set; }

    }
}
