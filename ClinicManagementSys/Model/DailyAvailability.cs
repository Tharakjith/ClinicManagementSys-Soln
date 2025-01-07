using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class DailyAvailability
{
    public int DailyAvailabilityId { get; set; }

    public int AvailabilityId { get; set; }

    public int AppointmentId { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual Availability Availability { get; set; } = null!;
}
