using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class AppointmentStatus
{
    public int AppointmentStatusId { get; set; }

    public string? AppointmentStatus1 { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
