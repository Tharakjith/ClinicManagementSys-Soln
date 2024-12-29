using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class DailyAvailability
{
    public int DailyAvailabilityId { get; set; }

    public int AvailabilityId { get; set; }

    public int PatientId { get; set; }

    public DateTime AvailableDate { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Availability Availability { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
