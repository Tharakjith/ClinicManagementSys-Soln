using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class Availability
{
    public int AvailabilityId { get; set; }

    public int? DoctorId { get; set; }

    public int? TimeSlotId { get; set; }

    public string? Session { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<DailyAvailability> DailyAvailabilities { get; set; } = new List<DailyAvailability>();

    public virtual Doctor? Doctor { get; set; }

    public virtual Timeslot? TimeSlot { get; set; }
}
