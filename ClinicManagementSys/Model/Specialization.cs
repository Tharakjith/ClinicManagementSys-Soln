using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class Specialization
{
    public int SpecializationId { get; set; }

    public string? SpecializationName { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
